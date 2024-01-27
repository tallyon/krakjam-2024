using UnityEngine;

[CreateAssetMenu]
public class SprintAbilityConfig : AbilityConfig
{
    [SerializeField] private float durationSeconds;
    [SerializeField] private float speedModifier;

    public float DurationSeconds => durationSeconds;
    public float SpeedModifier => speedModifier;
}