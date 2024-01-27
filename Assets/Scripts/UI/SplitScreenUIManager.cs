using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class SplitScreenUIManager : MonoBehaviour
    {
        [SerializeField] private List<PlayerActionsView> playerActions;

        //TODO: here subscribe to GameManager, and initialize and inform different playerAction set based on ID callback

        public void Start()
        {
            GameStateController.Instance.onPlayerJoined += SetupSplitScreen;
        }

        public void SetupSplitScreen(PlayerInput playerInput, PlayerCharacter playerCharacter)
        {
            if(playerInput.playerIndex < playerActions.Count)
                playerActions[playerInput.playerIndex].Setup(playerInput, playerCharacter);
        }
    }
}