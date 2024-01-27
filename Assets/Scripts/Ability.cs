using UnityEngine;

public class SprintAbility : Ability
{
    public float SpeedModifier => (Config as SprintAbilityConfig).SpeedModifier;
    public float Duration => (Config as SprintAbilityConfig).DurationSeconds;
    
    public SprintAbility(AbilityConfig config) : base(config)
    {
    }

    public SprintAbility(Ability ability) : base(ability.Config)
    {
        
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

    public GameObject Particles => Config.Particles;
    public string Name => Config.Name;

    public readonly AbilityConfig Config;

    public Ability(AbilityConfig config)
    {
        _cooldownSeconds = config.CooldownSeconds;
        Config = config;
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