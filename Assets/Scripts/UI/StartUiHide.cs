using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class StartUiHide : MonoBehaviour
    {
        [SerializeField] private TutorialUI tutorialUI;
        
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
            if (joinedPlayerCount >= 2)
            {

                var seq = DOTween.Sequence();
                seq.Append(GetComponent<CanvasGroup>().DOFade(0, 1));
                seq.onComplete += () =>
                {
                    Debug.Log("COMPLETE!");
                    tutorialUI.StartFadeIn();
                    gameObject.SetActive(false);
                };
                seq.Play();
            }
        }
    }
}