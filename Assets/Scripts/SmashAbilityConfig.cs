using UnityEngine;

[CreateAssetMenu]
public class SmashAbilityConfig : AbilityConfig
{
    [SerializeField] private float radius;
    [SerializeField] private float duration;

    public float Radius => radius;
    public float Duration => duration;
}