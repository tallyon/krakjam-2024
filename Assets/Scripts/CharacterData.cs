using UnityEngine;

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private CharacterTypeEnum characterType;
    [SerializeField] private GameObject prefab;

    [Header("Ability 1")]
    private Sprite ability1Sprite;
    [SerializeField] private int ability1CooldownSeconds;
    private AudioClip ability1Sound;
    
    [Header("Ability 2")]
    private Sprite ability2Sprite;
    [SerializeField] private int ability2CooldownSeconds;
    private AudioClip ability2Sound; 
    

    public GameObject Prefab => prefab;
    public CharacterTypeEnum CharacterType => characterType;
}
