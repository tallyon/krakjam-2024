using UnityEngine;

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