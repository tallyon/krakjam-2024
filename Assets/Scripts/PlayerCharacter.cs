using System;
using CartoonFX;
using UnityEngine;
using DG.Tweening;
using TMPro;

[RequireComponent(typeof(PlayerMovementController))]
public class PlayerCharacter : MonoBehaviour
{
    public CharacterTypeEnum characterTypeEnum;
    public CollectedItem collectedItem = null;
    public Action<ItemsData.ItemsEnum> onItemAdd;
    public Action onItemDeleted;
    public Action<Ability> onAbility1Used;
    public Action<Ability> onAbility2Used;

    [SerializeField] private TextMeshPro floatingTextPrefab;
    [SerializeField] private CFXR_Effect smashParticles;

    public CharacterData CharacterData => _characterData;

    [SerializeField] private CharacterData _characterData;

    public Ability Ability1 { get; set; }
    public Ability Ability2 { get; set; }

    private PlayerMovementController _playerMovementController;

    private void Awake()
    {
        _playerMovementController = GetComponent<PlayerMovementController>();
        _playerMovementController.CharacterMoveSpeedModifier = _characterData.MoveSpeed;
        Ability1 = new Ability(_characterData.Ability1CooldownSeconds);
        Ability2 = new Ability(_characterData.Ability2CooldownSeconds);
    }

    public void AddItem(ItemsData.ItemsEnum item)
    {
        var collected = GameStateController.Instance.GetCollectedItemPrefab(item);
        collectedItem = collected;
        onItemAdd?.Invoke(item);
    }

    public void DeleteItem()
    {
        collectedItem = null;
        onItemDeleted?.Invoke();
    }

    public void UseAbility1()
    {
        if (Ability1.State == Ability.AbilityState.OnCooldown) return;
        
        switch (_characterData.CharacterType)
        {
            case CharacterTypeEnum.Sigma:
                UseSkillSprint();
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
        
        switch (_characterData.CharacterType)
        {
            case CharacterTypeEnum.Sigma:
                UseSkillSmash();
                break;
            case CharacterTypeEnum.Beta:
                UseSkillVent();
                break;
            case CharacterTypeEnum.Both:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        Ability2.GoOnCooldown();
        onAbility2Used.Invoke(Ability2);
    }

    private void Update()
    {
        var timePassedSinceLastFrame = Time.deltaTime;
        Ability1.UpdateCooldownState(timePassedSinceLastFrame);
        Ability2.UpdateCooldownState(timePassedSinceLastFrame);
    }

    private void UseSkillSprint()
    {
        SpawnFloatingText("Sprint!");
    }

    private void UseSkillSmash()
    {
        SpawnFloatingText("Smash!");
        var particleInstance = Instantiate(smashParticles, transform.position, transform.rotation);
    }

    private void UseSkillObstacle()
    {
        SpawnFloatingText("Obstacle!");
    }

    private void UseSkillVent()
    {
        SpawnFloatingText("Vent!");
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