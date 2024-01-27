using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class EndGameScreen : MonoBehaviour
    {
        [SerializeField] private GameObject content;

        private void Start()
        {
            if (!GameStateController.Instance.IsGameInitialized)
                GameStateController.Instance.OnGameInit += SignToEvents;
            else
                SignToEvents();
        }

        private void SignToEvents()
        {
            GameStateController.Instance.Player1Score.OnPlayerWon += ShowEndGameScreenPlayer1;
            GameStateController.Instance.Player2Score.OnPlayerWon += ShowEndGameScreenPlayer2;
        }

        private void ShowEndGameScreenPlayer1()
        {
            //content.SetActive(true);
        }
        
        private void ShowEndGameScreenPlayer2()
        {
            content.SetActive(true);
        }

        
    }
}