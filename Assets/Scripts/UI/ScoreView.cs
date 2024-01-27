using UnityEngine;
using UnityEngine.UI;

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
                GameStateController.Instance.OnLevelStart += OnLevelStart;
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
            player1ScoreSlider.value = percent;
        }
        
        private void OnScore2Updated(int val, float percent)
        {
            player2ScoreSlider.value = percent;
        }
    }
}