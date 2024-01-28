using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(AudioSource))]
    public class EndGameScreen : MonoBehaviour
    {
        [SerializeField] private GameObject content;
        [SerializeField] private GameObject chad;
        [SerializeField] private GameObject nerd;
        [SerializeField] private GameObject noone;

        [SerializeField] private AudioClip loseSound;
        [SerializeField] private AudioClip winSound;
        private AudioSource _audio;

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
        }

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
            if (_audio != null && winSound != null) _audio.PlayOneShot(winSound);
        }
        
        private void ShowEndGameScreenPlayer2()
        {
            nerd.SetActive(true);
            if (_audio != null && winSound != null) _audio.PlayOneShot(winSound);
        }

        private void ShowEndGame()
        {
            noone.SetActive(true);
            if (_audio != null && loseSound != null) _audio.PlayOneShot(loseSound);            
        }

        
    }
}