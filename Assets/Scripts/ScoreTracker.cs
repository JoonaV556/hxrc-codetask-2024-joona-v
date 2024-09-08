using UnityEngine;

/// <summary>
/// Tracks player score
/// </summary>
public class ScoreTracker : MonoBehaviour
{
    public int Score = 0;

    void IncreaseScore()
    {
        Score++;
    }

    void ResetScore()
    {
        Score = 0;
    }
}
