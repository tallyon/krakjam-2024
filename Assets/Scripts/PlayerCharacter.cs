using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public enum FloatingTextColor
{
    Neutral,
    Positive,
    Negative
}

public enum PlayerCharacterStatus
{
    Normal,
    Stunned,
    InVent,
    ChoosingItem,
    Slipping,
    Interacting
}

[RequireComponent(typeof(PlayerMovementController))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCharacter : MonoBehaviour
{
    public CharacterTypeEnum characterTypeEnum => CharacterData.CharacterType;
    public CollectedItem collectedItem = null;
    public Action<CollectedItem> onItemAdd;
    public Action onItemDeleted;
    public Action<Ability> onAbility1Used;
    public Action<Ability> onAbility2Used;

    private PlayerCharacterStatus _playerStatus;
    public PlayerCharacterStatus PlayerStatus
    {
        get => _playerStatus;
        private set
        {
            var previousValue = _playerStatus;
            _playerStatus = value;
            OnPlayerCharacterStatusChanged?.Invoke(value, previousValue);
        }
    }

    private float _timeUntilStatusGoesBackToNormal;

    [SerializeField] private TextMeshPro floatingTextPrefab;
    [SerializeField] private GameObject stunParticlesPrefab;
    [SerializeField] private GameObject ventingParticlesPrefab;
    [SerializeField] private GameObject slippingParticlesPrefab;
    [SerializeField] private GameObject interactingParticlesPrefab;
    [SerializeField] private SpriteRenderer interactingFillingSpritePrefab;
    private GameObject _stunParticlesInstance;
    private GameObject _ventingParticlesInstance;
    private GameObject _slippingParticlesInstance;
    private GameObject _interactingParticlesInstance;
    private SpriteRenderer _interactingFillingSpriteInstance;
    
    [SerializeField] private SpriteRenderer characterSpriteRenderer;
    private int defaultSortOrder;
    
    /// <summary>
    /// Parameters are current status, previous status
    /// </summary>
    public Action<PlayerCharacterStatus, PlayerCharacterStatus> OnPlayerCharacterStatusChanged;

    public CharacterData CharacterData => _characterData;

    [SerializeField] private CharacterData _characterData;

    public Ability Ability1 { get; set; }
    public Ability Ability2 { get; set; }

    private PlayerMovementController _playerMovementController;

    private void Awake()
    {
        defaultSortOrder = characterSpriteRenderer.sortingOrder;
        _playerMovementController = GetComponent<PlayerMovementController>();
        _playerMovementController.CharacterMoveSpeedModifier = _characterData.MoveSpeed;
        Ability1 = new Ability(_characterData.Ability1Config);
        Ability2 = new Ability(_characterData.Ability2Config);
        
        OnPlayerCharacterStatusChanged += HandlePlayerStatusChanged;
    }

    private void HandlePlayerStatusChanged(PlayerCharacterStatus status, PlayerCharacterStatus previousStatus)
    {
        if (_stunParticlesInstance != null) Destroy(_stunParticlesInstance);
        if (_ventingParticlesInstance != null) Destroy(_ventingParticlesInstance);
        if (_slippingParticlesInstance != null) Destroy(_slippingParticlesInstance);
        if (_interactingFillingSpriteInstance != null) Destroy(_interactingFillingSpriteInstance);
        if (_interactingParticlesInstance != null) Destroy(_interactingParticlesInstance);

        Debug.Log($"{previousStatus} -> {status}");
        // if previously was in vent set sort order to 1
        if (previousStatus == PlayerCharacterStatus.InVent)
        {
            characterSpriteRenderer.sortingOrder = defaultSortOrder;
            _ventingParticlesInstance = Instantiate(ventingParticlesPrefab, transform.position, transform.rotation);
        }
        
        switch (status)
        {
            case PlayerCharacterStatus.Normal:
                Debug.Log($"NORMAL on {gameObject.name}!");
                break;
            case PlayerCharacterStatus.Stunned:
                Debug.Log($"STUN on {gameObject.name}!");
                _stunParticlesInstance = Instantiate(stunParticlesPrefab, transform.position + new Vector3(0, 3, -10), transform.rotation);
                _stunParticlesInstance.transform.SetParent(transform, true);
                break;
            case PlayerCharacterStatus.InVent:
                Debug.Log($"IN VENT on {gameObject.name}");
                characterSpriteRenderer.sortingOrder = -10;
                _ventingParticlesInstance = Instantiate(ventingParticlesPrefab, transform.position, transform.rotation);
                break;
            case PlayerCharacterStatus.Slipping:
                Debug.Log($"SLIPPING on {gameObject.name}!");
                _slippingParticlesInstance = Instantiate(slippingParticlesPrefab, transform.position + new Vector3(0, 3, -10), transform.rotation);
                _slippingParticlesInstance.transform.SetParent(transform, true);
                var targetVector = _playerMovementController.MoveVelocity.normalized * 10;
                _playerMovementController.Rigidbody.DOMove(_playerMovementController.transform.position + new Vector3(targetVector.x, targetVector.y, 0),
                        PlayerMovementController.STUNNING_SURFACE_STUN_DURATION_SECONDS)
                    .SetEase(Ease.OutSine).Play();
                break;
            case PlayerCharacterStatus.Interacting:
                Debug.Log($"INTERACTING on {gameObject.name}!");
                _interactingParticlesInstance = Instantiate(interactingParticlesPrefab, transform.position + new Vector3(0, 3, -10), transform.rotation);
                _interactingParticlesInstance.transform.SetParent(transform, true);
                break;
        }
    }

    public void AddItem(ItemsData.ItemsEnum item)
    {
        var collected = GameStateController.Instance.GetCollectedItemPrefab(item);
        collectedItem = collected;
        onItemAdd?.Invoke(collectedItem);
    }

    public void DeleteItem()
    {
        collectedItem = null;
        onItemDeleted?.Invoke();
    }

    public void UseAbility1()
    {
        if (Ability1.State == Ability.AbilityState.OnCooldown) return;

        var abilityName = Ability1.Name;
        var particles = Ability1.Particles;
        if (particles != null) Instantiate(particles, transform.position, transform.rotation);

        var wasSkillUSed = false;
        
        switch (_characterData.CharacterType)
        {
            case CharacterTypeEnum.Sigma:
                var sprintAbility = new SprintAbility(Ability1);
                wasSkillUSed = UseSkillSprint(sprintAbility);
                break;
            case CharacterTypeEnum.Beta:
                wasSkillUSed = UseSkillObstacle();
                break;
            case CharacterTypeEnum.Both:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (wasSkillUSed)
        {
            Ability1.GoOnCooldown();
            onAbility1Used.Invoke(Ability1);
            if (string.IsNullOrEmpty(abilityName) == false) SpawnFloatingText($"{abilityName}!", FloatingTextColor.Positive);
        }
        else
        {
            if (string.IsNullOrEmpty(abilityName) == false) SpawnFloatingText($"{abilityName}?", FloatingTextColor.Negative);
        }
    }

    public void UseAbility2()
    {
        if (Ability2.State == Ability.AbilityState.OnCooldown) return;

        var abilityName = Ability2.Name;
        var particles = Ability2.Particles;
        if (particles != null) Instantiate(particles, transform.position, transform.rotation);

        var wasSkillUsed = false;
        switch (_characterData.CharacterType)
        {
            case CharacterTypeEnum.Sigma:
                var smashAbility = new SmashAbility(Ability2);
                wasSkillUsed = UseSkillSmash(smashAbility);
                _playerMovementController.SmashItem();
                break;
            case CharacterTypeEnum.Beta:
                var ventAbility = new VentAbility(Ability2);
                wasSkillUsed = UseSkillVent(ventAbility);
                break;
            case CharacterTypeEnum.Both:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (wasSkillUsed)
        {
            Ability2.GoOnCooldown();
            onAbility2Used.Invoke(Ability2);
            if (string.IsNullOrEmpty(abilityName) == false) SpawnFloatingText($"{abilityName}!", FloatingTextColor.Positive);
        }
        else
        {
            if (string.IsNullOrEmpty(abilityName) == false) SpawnFloatingText($"{abilityName}?", FloatingTextColor.Negative);
        }
    }

    public void ApplyStatus(PlayerCharacterStatus status, float timeSeconds)
    {
        ApplyStatus(status);
        StartCoroutine(ReturnStatusToNormal(timeSeconds));
    }

    public void ApplyStatus(PlayerCharacterStatus status)
    {
        PlayerStatus = status;
    }

    public void HandleInteractionHold()
    {
        SpawnFloatingText("INTERACTION HOLD!");
    }

    public void UseItem()
    {
        SpawnFloatingText("USING ITEM!");
    }

    private IEnumerator ReturnStatusToNormal(float timeSeconds)
    {
        yield return new WaitForSeconds(timeSeconds);
        ApplyStatus(PlayerCharacterStatus.Normal);
    }

    private void Update()
    {
        var timePassedSinceLastFrame = Time.deltaTime;
        Ability1.UpdateCooldownState(timePassedSinceLastFrame);
        Ability2.UpdateCooldownState(timePassedSinceLastFrame);
    }

    private bool UseSkillSprint(SprintAbility sprintAbility)
    {
        var previousSpeedModifier = _playerMovementController.CharacterMoveSpeedModifier;
        StartCoroutine(RestorePreviousSpeedModifier(previousSpeedModifier, sprintAbility.Duration));
        
        var newSpeedModifier = _playerMovementController.CharacterMoveSpeedModifier * sprintAbility.SpeedModifier;
        _playerMovementController.CharacterMoveSpeedModifier = newSpeedModifier;
        
        return true;
    }

    private IEnumerator RestorePreviousSpeedModifier(float speedModifier, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        _playerMovementController.CharacterMoveSpeedModifier = speedModifier;
    }

    private bool UseSkillSmash(SmashAbility abilityConfig)
    {
        // check if other player is in the radius of the skill
        var otherPlayer = GameStateController.Instance.GetOtherPlayer(this);
        var distanceToOtherPlayer = Vector2.Distance(otherPlayer.transform.position, transform.position);

        if (distanceToOtherPlayer > abilityConfig.Radius) return false;
        
        // check if other player is in normal status
        if (otherPlayer.PlayerStatus != PlayerCharacterStatus.Normal) return false;
        
        // if other player is in radius apply Stunned status and push him back
        otherPlayer.ApplyStatus(PlayerCharacterStatus.Stunned, abilityConfig.Duration);
        otherPlayer._playerMovementController.Rigidbody.DOMove(otherPlayer.transform.position +
                                     (otherPlayer.transform.position - transform.position), .5f);

        return true;
    }

    private bool UseSkillObstacle()
    {
        return _playerMovementController.CloseDoor();
    }

    private bool UseSkillVent(VentAbility ventAbility)
    {
        return _playerMovementController.EnterVent(ventAbility.TravelDuration);
    }

    private void SpawnFloatingText(string text, FloatingTextColor color = FloatingTextColor.Neutral)
    {
        var floatingText = Instantiate(floatingTextPrefab);
        floatingText.text = text;
        floatingText.transform.position = transform.position;
        switch (color)
        {
            case FloatingTextColor.Neutral:
                floatingText.color = Color.white;
                break;
            case FloatingTextColor.Positive:
                floatingText.color = Color.green;
                break;
            case FloatingTextColor.Negative:
                floatingText.color = Color.red;
                break;
        }
        var seq = DOTween.Sequence();
        var textColor = floatingText.color;
        floatingText.color = new Color(textColor.r, textColor.g, textColor.b, 0);
        textColor.a = 1;
        var fadeIn = floatingText.DOColor(textColor, .25f);
        var moveUp = floatingText.transform.DOMoveY(floatingText.transform.position.y + 5, .25f)
            .SetEase(Ease.OutSine);
        seq.Append(fadeIn);
        seq.Join(moveUp);
        seq.AppendInterval(.5f);
        var fadeOutColorText = new Color(floatingText.color.r, floatingText.color.g, floatingText.color.b, 0);
        var fadeOut = floatingText.DOColor(fadeOutColorText, .25f);
        seq.Append(fadeOut);
        seq.Play();
        seq.onComplete += () => Destroy(floatingText.gameObject);
    }
}