using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text ScoreText;
    public GameObject RestartUI;

    private void OnEnable()
    {
        // Update score when score increases
        ScoreTracker.OnScoreIncreased += UpdateScore;

        // Show restart screen when game over
        GameManager.OnGameOver += ShowRestartWindow;
    }

    private void OnDisable()
    {
        ScoreTracker.OnScoreIncreased -= UpdateScore;

        GameManager.OnGameOver -= ShowRestartWindow;
    }

    void UpdateScore(int newScore)
    {
        ScoreText.text = newScore.ToString();
    }

    void ShowRestartWindow()
    {
        RestartUI.SetActive(true);
    }
}
