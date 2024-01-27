using CartoonFX;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class AbilityConfig : ScriptableObject
{
    [FormerlySerializedAs("name")] [SerializeField] private string abilityName;
    [SerializeField] private int cooldownSeconds;
    [SerializeField] private GameObject particles;

    public string Name => abilityName;
    public int CooldownSeconds => cooldownSeconds;
    public GameObject Particles => particles;
}