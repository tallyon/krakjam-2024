using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using static ItemsData;
using Random = UnityEngine.Random;

public class GameStateController : Singleton<GameStateController>
{
    private const int SCORE_REQUIRED_TO_WIN = 20;
    
    public Dictionary<string, CharacterTypeEnum> playersCharacter = new();
    public List<CharacterData> charactersPrefab;
    public Action<PlayerInput, PlayerCharacter> onPlayerJoined;
    public Action OnGameStart;
    public Action<float> OnStartTimerTick;
    [FormerlySerializedAs("OnGameEnd")] public Action OnGameTimerEnd;
    public Action<float> OnGameTimerTick;
    public Action OnGameInit;
    public int roundTime = 300;
    private int startTime = 3; // set to 17 if tutorial is enabled
    public bool IsGameInitialized { get; private set; }
    [SerializeField] private List<LevelConfig> LevelConfigs;
    [SerializeField] private ItemsData ItemsConfig;
    [SerializeField] private PlayerInputManager playerInputManagerPrefab;

    private List<GameObject> currentLevelSpawnedObjects = new();
    private CharacterTypeEnum? _player1Character;
    private CharacterTypeEnum? _player2Character;
    private PlayerCharacter _player1;
    private PlayerCharacter _player2;

    public PlayerScore Player1Score { get; private set; }
    public PlayerScore Player2Score { get; private set; }

    private void Start()
    {
        Player1Score = new PlayerScore(SCORE_REQUIRED_TO_WIN);
        Player2Score = new PlayerScore(SCORE_REQUIRED_TO_WIN);
        
        _player1Character = charactersPrefab[0].CharacterType;
        _player2Character = charactersPrefab[1].CharacterType;
        StartLevel(0);

        IsGameInitialized = true;
        OnGameInit?.Invoke();
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(1);
    }

    private IEnumerator BeginStartCountdown()
    {
        float timer = startTime;
        while (timer > 0)
        {
            OnStartTimerTick?.Invoke(timer);
            yield return new WaitForSeconds(1);
            timer--;
        }
        OnStartTimerTick?.Invoke(0);
        OnGameStart?.Invoke();
        StartCoroutine(BeginGameCountdown());
    }

    private IEnumerator BeginGameCountdown()
    {
        int timer = roundTime;
        while (timer > 0)
        {
            OnGameTimerTick?.Invoke(timer);
            yield return new WaitForSeconds(1);
            timer--;
        }
        OnGameTimerTick?.Invoke(0);
        OnGameTimerEnd?.Invoke();
    }
    
    private IEnumerator SimulateScore()
    {
        while (Player1Score.PercentageVictoryAchieved < 1 && Player2Score.PercentageVictoryAchieved < 1)
        {
            Player1Score.AddScore(Random.Range(1,3));
            Player2Score.AddScore(Random.Range(1,3));
            yield return new WaitForSeconds(1);
        }
    }

    private bool IsEachCharacterSelected()
    {
        if (_player1Character.HasValue == false)
        {
            Debug.LogError("Player 1 did not choose a character!");
            return false;
        }

        if (_player2Character.HasValue == false)
        {
            Debug.LogError("Player 2 did not choose a character!");
            return false;
        }

        if (_player1Character.Value == _player2Character.Value)
        {
            Debug.LogError("Player cannot choose the same character!");
            return false;
        }

        return true;
    }

    public void StartLevel(int index)
    {
        if (!IsEachCharacterSelected())
        {
            Debug.LogError($"Not each character is selected");
            return;
        }
        
        // destroy all spawned objects from current level if any
        foreach (var go in currentLevelSpawnedObjects)
        {
            Destroy(go);
        }
        currentLevelSpawnedObjects.Clear();

        var currentLevelConfig = LevelConfigs[index];

        // spawn level
        var levelInstance = Instantiate(currentLevelConfig.LevelPrefab);
        currentLevelSpawnedObjects.Add(levelInstance);
        
        // spawn players
        var player1 = Instantiate(charactersPrefab[0].Prefab);
        var player2 = Instantiate(charactersPrefab[1].Prefab);
        currentLevelSpawnedObjects.Add(player1);
        currentLevelSpawnedObjects.Add(player2);

        _player1 = player1.GetComponent<PlayerCharacter>();
        _player2 = player2.GetComponent<PlayerCharacter>();

        var inputManager = Instantiate(playerInputManagerPrefab);
        inputManager.onPlayerJoined += OnPlayerJoined;
        currentLevelSpawnedObjects.Add(inputManager.gameObject);
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        if (playerInput.playerIndex == 0)
        {
            StartCoroutine(BeginStartCountdown());
            onPlayerJoined.Invoke(playerInput, _player1); // remove this
            //TODO: USE ON PLAYER 2 LAATER
        }
        else if (playerInput.playerIndex == 1)
        {
            StartCoroutine(BeginStartCountdown());
            onPlayerJoined.Invoke(playerInput, _player2);
            StartCoroutine(BeginStartCountdown());
        }
    }

    public PlayerCharacter GetPlayerObject(CharacterTypeEnum characterEnum)
    {
        if (_player1.characterTypeEnum == characterEnum) return _player1;
        return _player2;
    }

    public InteractableItem GetInteractableItemPrefab(ItemsData.ItemsEnum itemsEnum)
    {
        return ItemsConfig.GetInteractableItemPrefab(itemsEnum);
    }

    public List<ItemData> GetItemsData()
    {
        return ItemsConfig.items;
    }

    public List<SpecialGroupItems> GetGroupsData()
    {
        return ItemsConfig.itemsGroup;
    }

    public CollectedItem GetCollectedItemPrefab(ItemsData.ItemsEnum itemsEnum)
    {
        return ItemsConfig.GetCollectedItemPrefab(itemsEnum);
    }

    public PlayerCharacter GetOtherPlayer(PlayerCharacter playerAsking)
    {
        return playerAsking == _player1 ? _player2 : _player1;
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        var gamepad = Gamepad.current;

        if (keyboard != null && keyboard.escapeKey.wasPressedThisFrame ||
            gamepad != null && gamepad.startButton.wasPressedThisFrame)
        {
            QuitGame();
        }
    }
    
    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}

public enum CharacterTypeEnum
{
    Sigma,
    Beta,
    Both
}

public struct PlayerData
{
    public PlayerInput playerInput;
    public PlayerCharacter playerCharacter;

    public PlayerData(PlayerInput playerInput, PlayerCharacter playerCharacter)
    {
        this.playerInput = playerInput;
        this.playerCharacter = playerCharacter;
    }
}
