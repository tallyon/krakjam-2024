using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public enum PlayerCharacterStatus
{
    Normal,
    Stunned,
    InVent,
    ChoosingItem
}

[RequireComponent(typeof(PlayerMovementController))]
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
            _playerStatus = value;
            OnPlayerCharacterStatusChanged?.Invoke(value);
        }
    }

    [SerializeField] private TextMeshPro floatingTextPrefab;
    [SerializeField] private GameObject stunParticlesPrefab;
    private GameObject _stunParticlesInstance;
    
    public Action<PlayerCharacterStatus> OnPlayerCharacterStatusChanged;

    public CharacterData CharacterData => _characterData;

    [SerializeField] private CharacterData _characterData;

    public Ability Ability1 { get; set; }
    public Ability Ability2 { get; set; }

    private PlayerMovementController _playerMovementController;

    private void Awake()
    {
        _playerMovementController = GetComponent<PlayerMovementController>();
        _playerMovementController.CharacterMoveSpeedModifier = _characterData.MoveSpeed;
        Ability1 = new Ability(_characterData.Ability1Config);
        Ability2 = new Ability(_characterData.Ability2Config);
        
        OnPlayerCharacterStatusChanged += HandlePlayerStatusChanged;
    }

    private void HandlePlayerStatusChanged(PlayerCharacterStatus status)
    {
        if (_stunParticlesInstance != null) Destroy(_stunParticlesInstance);

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
        if (string.IsNullOrEmpty(abilityName) == false) SpawnFloatingText($"{abilityName}!");
        if (particles != null) Instantiate(particles, transform.position, transform.rotation);
        
        switch (_characterData.CharacterType)
        {
            case CharacterTypeEnum.Sigma:
                var sprintAbility = new SprintAbility(Ability1);
                UseSkillSprint(sprintAbility);
                break;
            case CharacterTypeEnum.Beta:
                UseSkillObstacle();
                break;
            case CharacterTypeEnum.Both:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        Ability1.GoOnCooldown();
        onAbility1Used.Invoke(Ability1);
    }

    public void UseAbility2()
    {
        if (Ability2.State == Ability.AbilityState.OnCooldown) return;

        var abilityName = Ability2.Name;
        var particles = Ability2.Particles;
        if (string.IsNullOrEmpty(abilityName) == false) SpawnFloatingText($"{abilityName}!");
        if (particles != null) Instantiate(particles, transform.position, transform.rotation);

        switch (_characterData.CharacterType)
        {
            case CharacterTypeEnum.Sigma:
                var smashAbility = new SmashAbility(Ability2);
                UseSkillSmash(smashAbility);
                break;
            case CharacterTypeEnum.Beta:
                var ventAbility = new VentAbility(Ability2);
                UseSkillVent(ventAbility);
                break;
            case CharacterTypeEnum.Both:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        Ability2.GoOnCooldown();
        onAbility2Used.Invoke(Ability2);
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

    public void ChooseItem1()
    {
        Debug.Log("Choosing item 1");
    }

    public void ChooseItem2()
    {
        Debug.Log("Choosing item 2");
    }

    public void ChooseItem3()
    {
        Debug.Log("Choosing item 3");
    }

    public void HandleInteractionHold()
    {
        SpawnFloatingText("INTERACTION HOLD!");
    }

    private IEnumerator ReturnStatusToNormal(float timeSeconds)
    {
        yield return new WaitForSeconds(10);
        ApplyStatus(PlayerCharacterStatus.Normal);
    }

    private void Update()
    {
        var timePassedSinceLastFrame = Time.deltaTime;
        Ability1.UpdateCooldownState(timePassedSinceLastFrame);
        Ability2.UpdateCooldownState(timePassedSinceLastFrame);
    }

    private void UseSkillSprint(SprintAbility sprintAbility)
    {
        var previousSpeedModifier = _playerMovementController.CharacterMoveSpeedModifier;
        StartCoroutine(RestorePreviousSpeedModifier(previousSpeedModifier, sprintAbility.Duration));
        
        var newSpeedModifier = _playerMovementController.CharacterMoveSpeedModifier * sprintAbility.SpeedModifier;
        _playerMovementController.CharacterMoveSpeedModifier = newSpeedModifier;
    }

    private IEnumerator RestorePreviousSpeedModifier(float speedModifier, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        _playerMovementController.CharacterMoveSpeedModifier = speedModifier;
    }

    private void UseSkillSmash(SmashAbility abilityConfig)
    {
        // check if other player is in the radius of the skill
        var otherPlayer = GameStateController.Instance.GetOtherPlayer(this);
        var distanceToOtherPlayer = Vector2.Distance(otherPlayer.transform.position, transform.position);

        if (distanceToOtherPlayer > abilityConfig.Radius) return;
        
        // if other player is in radius apply Stunned status and push him back
        otherPlayer.ApplyStatus(PlayerCharacterStatus.Stunned, abilityConfig.Duration);
        otherPlayer._playerMovementController.Rigidbody.DOMove(otherPlayer.transform.position +
                                     (otherPlayer.transform.position - transform.position), .5f);
    }

    private void UseSkillObstacle()
    {
        SpawnFloatingText("Obstacle!");
    }

    private void UseSkillVent(VentAbility ventAbility)
    {
        _playerMovementController.EnterVent(ventAbility.TravelDuration);
    }

    private void SpawnFloatingText(string text)
    {
        var floatingText = Instantiate(floatingTextPrefab);
        floatingText.text = text;
        floatingText.transform.position = transform.position;
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