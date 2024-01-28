using UnityEngine;

[CreateAssetMenu]
public class VentAbilityConfig : AbilityConfig
{
    [SerializeField] private float travelDuration;
    public float TravelDuration => travelDuration;
}
