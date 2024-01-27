using System;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public CharacterTypeEnum characterTypeEnum;
    public CollectedItem collectedItem = null;
    public Action<ItemsData.ItemsEnum> onItemAdd;
    public Action onItemDeleted;
    public Action<Ability> onAbility1Used;
    public Action<Ability> onAbility2Used;

    public CharacterData CharacterData => _characterData;

    [SerializeField] private CharacterData _characterData;

    public Ability Ability1 { get; set; }
    public Ability Ability2 { get; set; }

    private void Awake()
    {
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
        Ability1.GoOnCooldown();
        onAbility1Used.Invoke(Ability1);
    }

    public void UseAbility2()
    {
        Ability2.GoOnCooldown();
        onAbility2Used.Invoke(Ability2);
    }

    private void Update()
    {
        var timePassedSinceLastFrame = Time.deltaTime;
        Ability1.UpdateCooldownState(timePassedSinceLastFrame);
        Ability2.UpdateCooldownState(timePassedSinceLastFrame);
    }
}

public class Ability
{
    public enum AbilityState
    {
        Ready,
        OnCooldown
    };

    public AbilityState State { get; set; } = AbilityState.Ready;
    public float CooldownLeftSeconds { get; private set; }

    private readonly int _cooldownSeconds;

    public Ability(int cooldownSeconds)
    {
        _cooldownSeconds = cooldownSeconds;
    }
    
    public void GoOnCooldown()
    {
        if (State != AbilityState.Ready)
        {
            Debug.Log("Cannot use ability on cooldown!");
            return;
        }
        
        State = AbilityState.OnCooldown;
        CooldownLeftSeconds = _cooldownSeconds;
        Debug.Log($"Cooldown is {CooldownLeftSeconds} seconds");
    }

    public void UpdateCooldownState(float secondsPassedSinceLastFrame)
    {
        if (State != AbilityState.OnCooldown) return;
        
        CooldownLeftSeconds -= secondsPassedSinceLastFrame;
        
        if (CooldownLeftSeconds <= 0)
        {
            State = AbilityState.Ready;
        }
    }
}
