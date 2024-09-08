using UnityEngine;

/// <summary>
/// Tracks player score
/// </summary>
public class ScoreTracker : MonoBehaviour
{
    public int Score = 0;

    private void Start()
    {
        Score = 0; // Reset score on game start & on reset (Reset reloads scene, thus Start() is called again)
    }

    void IncreaseScore()
    {
        Score++;
        Debug.Log($"Score increased. Score: {Score}");
    }

    void ResetScore()
    {
        Score = 0;
        Debug.Log($"Score reset. Score: {Score}");
    }
}
