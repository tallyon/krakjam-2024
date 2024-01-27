using UnityEngine;

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private CharacterTypeEnum characterType;
    [SerializeField] private GameObject prefab;

    [Header("Ability 1")]
    [SerializeField] private Sprite ability1Sprite;
    [SerializeField] private int ability1CooldownSeconds;
    [SerializeField] private AudioClip ability1Sound;
    
    [Header("Ability 2")]
    [SerializeField] private Sprite ability2Sprite;
    [SerializeField] private int ability2CooldownSeconds;
    [SerializeField] private AudioClip ability2Sound; 
    

    public GameObject Prefab => prefab;
    public CharacterTypeEnum CharacterType => characterType;

    public Sprite Ability1Sprite => ability1Sprite;
    public Sprite Ability2Sprite => ability2Sprite;

    public int Ability1CooldownSeconds => ability1CooldownSeconds;
    public int Ability2CooldownSeconds => ability2CooldownSeconds;
}
