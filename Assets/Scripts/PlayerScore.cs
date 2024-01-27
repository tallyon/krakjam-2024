using System;

public class PlayerScore
{
    public int CurrentScore { get; private set; }
    public int MaxScore { get; private set; }
    public float PercentageVictoryAchieved => CurrentScore / (float)MaxScore;

    /// <summary>
    /// Current score value and percentage (0-1) victory achieved is provided.
    /// </summary>
    public Action<int, float> OnScoreUpdated;

    public PlayerScore(int maxScore)
    {
        MaxScore = maxScore;
    }

    public void AddScore(int score)
    {
        CurrentScore += score;
        OnScoreUpdated?.Invoke(CurrentScore, PercentageVictoryAchieved);
    }
}
