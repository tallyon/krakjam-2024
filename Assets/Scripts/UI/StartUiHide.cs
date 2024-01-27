using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public class StartUiHide : MonoBehaviour
    {
        private int joinedPlayerCount = 0;
        private void Start()
        {
            if (GameStateController.Instance.IsGameInitialized)
            {
                GameStateController.Instance.onPlayerJoined += OnJoin;
            }
            else
            {
                GameStateController.Instance.OnGameInit += () => GameStateController.Instance.onPlayerJoined += OnJoin;
            }
        }
        
        private void OnJoin(PlayerInput input, PlayerCharacter playerCharacter)
        {
            joinedPlayerCount++;
            if(joinedPlayerCount >= 2)
                gameObject.SetActive(false);
        }
    }
}