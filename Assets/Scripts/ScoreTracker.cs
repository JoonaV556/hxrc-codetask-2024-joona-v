using UnityEngine;

/// <summary>
/// Tracks player score
/// </summary>
public class ScoreTracker : MonoBehaviour
{
    public int Score = 0;

    private void OnEnable()
    {
        ScoreConsumable.OnScoreConsumableConsumed += IncreaseScore; // Increase score when score consumable is consumed :DD
    }

    private void OnDisable()
    {
        ScoreConsumable.OnScoreConsumableConsumed -= IncreaseScore;
    }

    private void Start()
    {
        ResetScore(); // Reset score on game start & on restart (Reset reloads scene, thus Start() is called again)
    }

    void IncreaseScore(ConsumeInfo consumeInfo)
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
