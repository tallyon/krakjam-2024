using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private Slider player1ScoreSlider;
        [SerializeField] private Slider player2ScoreSlider;

        private void Start()
        {
            if (!GameStateController.Instance.IsLevelStarted)
            {
                GameStateController.Instance.OnLevelStart += onLevelStart;
                return;
            }
            onLevelStart();
        }

        private void onLevelStart()
        {
            GameStateController.Instance.Player1Score.OnScoreUpdated += OnScore1Updated;
            GameStateController.Instance.Player2Score.OnScoreUpdated += OnScore2Updated;
        }
        private void OnScore1Updated(int val, float percent)
        {
            PlaySliderTweenAnimation(player1ScoreSlider, percent);
        }
        
        private void OnScore2Updated(int val, float percent)
        {
            PlaySliderTweenAnimation(player2ScoreSlider, percent);
        }

        private void PlaySliderTweenAnimation(Slider targetSlider, float targetValue)
        {
            player1ScoreSlider.DOValue(targetValue, 0.5f);
            var scaleTween = targetSlider.targetGraphic.GetComponent<RectTransform>()
                .DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.25f);
            scaleTween.SetLoops(2, LoopType.Yoyo);
            scaleTween.Play();
        }
        
    }
}