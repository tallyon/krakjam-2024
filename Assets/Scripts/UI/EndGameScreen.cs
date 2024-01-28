using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class EndGameScreen : MonoBehaviour
    {
        [SerializeField] private GameObject content;
        [SerializeField] private GameObject chad;
        [SerializeField] private GameObject nerd;
        [SerializeField] private GameObject noone;

        private void Start()
        {
            chad.SetActive(false);
            nerd.SetActive(false);
            noone.SetActive(false);
            if (!GameStateController.Instance.IsGameInitialized)
                GameStateController.Instance.OnGameInit += SignToEvents;
            else
                SignToEvents();
        }

        private void SignToEvents()
        {
            GameStateController.Instance.Player1Score.OnPlayerWon += ShowEndGameScreenPlayer1;
            GameStateController.Instance.Player2Score.OnPlayerWon += ShowEndGameScreenPlayer2;
            GameStateController.Instance.OnGameTimerEnd += ShowEndGame;
        }

        private void ShowEndGameScreenPlayer1()
        {
            chad.SetActive(true);
        }
        
        private void ShowEndGameScreenPlayer2()
        {
            nerd.SetActive(true);
        }

        private void ShowEndGame()
        {
            noone.SetActive(true);
        }

        
    }
}