using System;
using UnityEngine;

public class PlayerScore
{
    public int CurrentScore { get; private set; }
    public int MaxScore { get; private set; }
    /// <summary>
    /// Returns percentage score victory achieved 0.0 - 1.0 (0% - 100%)
    /// </summary>
    public float PercentageVictoryAchieved => Mathf.Clamp(CurrentScore / (float)MaxScore, 0, 1);

    /// <summary>
    /// Current score value and percentage (0-1) victory achieved is provided.
    /// </summary>
    public Action<int, float> OnScoreUpdated;

    public Action OnPlayerWon;

    public PlayerScore(int maxScore)
    {
        MaxScore = maxScore;
    }

    public void AddScore(int score)
    {
        CurrentScore += score;
        if(CurrentScore >= MaxScore)
            OnPlayerWon?.Invoke();
        
        OnScoreUpdated?.Invoke(CurrentScore, PercentageVictoryAchieved);
    }
}
