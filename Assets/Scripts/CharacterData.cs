using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    [FormerlySerializedAs("name")] [SerializeField] private string characterName;
    [SerializeField] private CharacterTypeEnum characterType;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float moveSpeed = 1.0f;

    [Header("Ability 1")]
    [SerializeField] private AbilityConfig ability1Config;
    [SerializeField] private Sprite ability1Sprite;
    [SerializeField] private int ability1CooldownSeconds;
    [SerializeField] private AudioClip ability1Sound;
    
    [Header("Ability 2")]
    [SerializeField] private AbilityConfig ability2Config;
    [SerializeField] private Sprite ability2Sprite;
    [SerializeField] private int ability2CooldownSeconds;
    [SerializeField] private AudioClip ability2Sound; 
    
    public GameObject Prefab => prefab;
    public CharacterTypeEnum CharacterType => characterType;

    public Sprite Ability1Sprite => ability1Sprite;
    public Sprite Ability2Sprite => ability2Sprite;

    public int Ability1CooldownSeconds => ability1CooldownSeconds;
    public int Ability2CooldownSeconds => ability2CooldownSeconds;

    public float MoveSpeed => moveSpeed;

    public AbilityConfig Ability1Config => ability1Config;
    public AbilityConfig Ability2Config => ability2Config;
}
