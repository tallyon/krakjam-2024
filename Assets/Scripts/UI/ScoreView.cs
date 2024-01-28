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
            if (!GameStateController.Instance.IsGameInitialized)
            {
                GameStateController.Instance.OnGameInit += OnLevelStart;
                return;
            }
            OnLevelStart();
        }

        private void OnLevelStart()
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
            PlaySlider2TweenAnimation(player2ScoreSlider, percent);
        }

        private void PlaySliderTweenAnimation(Slider targetSlider, float targetValue)
        {
            player1ScoreSlider.DOValue(targetValue, 0.5f);
            var scaleTween = targetSlider.targetGraphic.GetComponent<RectTransform>()
                .DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.25f);
            scaleTween.SetLoops(2, LoopType.Yoyo);
            scaleTween.Play();
        }
        
        private void PlaySlider2TweenAnimation(Slider targetSlider, float targetValue)
        {
            player2ScoreSlider.DOValue(targetValue, 0.5f);
            var scaleTween = targetSlider.targetGraphic.GetComponent<RectTransform>()
                .DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.25f);
            scaleTween.SetLoops(2, LoopType.Yoyo);
            scaleTween.Play();
        }
        
    }
}