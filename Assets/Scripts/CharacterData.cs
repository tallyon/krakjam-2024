using UnityEngine;

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private CharacterTypeEnum characterType;
    [SerializeField] private GameObject prefab;

    public GameObject Prefab => prefab;
    public CharacterTypeEnum CharacterType => characterType;
}
