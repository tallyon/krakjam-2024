using UnityEngine;

[CreateAssetMenu]
public class LevelConfig : ScriptableObject
{
    public GameObject LevelPrefab;
    public Vector2 Player1SpawnPosition;
    public Vector2 Player2SpawnPosition;
}
