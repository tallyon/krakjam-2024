using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static ItemsData;

public class GameStateController : Singleton<GameStateController>
{
    public Dictionary<string, CharacterTypeEnum> playersCharacter = new();
    public List<CharacterData> charactersPrefab;
    public Action<PlayerInput, PlayerCharacter> onPlayerJoined;
    [SerializeField] private List<LevelConfig> LevelConfigs;
    [SerializeField] private ItemsData ItemsConfig;
    [SerializeField] private PlayerInputManager playerInputManagerPrefab;

    private List<GameObject> currentLevelSpawnedObjects = new();
    private CharacterTypeEnum? _player1Character;
    private CharacterTypeEnum? _player2Character;
    private PlayerCharacter _player1;
    private PlayerCharacter _player2;
    
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _player1Character = charactersPrefab[0].CharacterType;
        _player2Character = charactersPrefab[1].CharacterType;
        StartLevel(0);
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
        if(playerInput.playerIndex == 0)
            onPlayerJoined.Invoke(playerInput, _player1);
        else if(playerInput.playerIndex == 1)
            onPlayerJoined.Invoke(playerInput, _player2);
    }
    
    public void OnInteract(Interaction interaction, CharacterTypeEnum characterEnum)
    {
        //var interactingPlayer = characterEnum == CharacterTypeEnum.Beta ? _player1 : _player2;

        //switch (interaction)
        //{
        //    case TakeItemInteraction takeItemInteraction:
        //        interactingPlayer.AddItem(takeItemInteraction.item);
        //}
    }

    public PlayerCharacter GetPlayerObject(CharacterTypeEnum characterEnum)
    {
        var interactingPlayer = characterEnum == CharacterTypeEnum.Beta ? _player1 : _player2;

        return interactingPlayer;
    }

    public InteractableItem GetInteractableItemPrefab(ItemsEnum itemsEnum)
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

    public CollectedItem GetCollectedItemPrefab(ItemsEnum itemsEnum)
    {
        return ItemsConfig.GetCollectedItemPrefab(itemsEnum);
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
