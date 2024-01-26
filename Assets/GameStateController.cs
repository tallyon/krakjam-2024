using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameStateController : Singleton<GameStateController>
{
    public Dictionary<string, CharacterTypeEnum> playersCharacter = new();
    public List<CharacterData> charactersPrefab;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void SetPlayersCharacter(Dictionary<string, CharacterTypeEnum> toSet)
    {
        playersCharacter = toSet;
    }

    public bool IsEachCharacterSelected()
    {
        if (playersCharacter.Count <= 0)
            return false;
        return playersCharacter.Values.Contains(CharacterTypeEnum.Sigma) &&
               playersCharacter.Values.Contains(CharacterTypeEnum.Beta);
    }

    public void StartLevel()
    {
        if (!IsEachCharacterSelected())
        {
            Debug.LogError($"Not each character is selected");
            return;
        }
        
        SceneManager.LoadScene("TestMap");
    }
}

public enum CharacterTypeEnum
{
    Sigma,
    Beta
}
