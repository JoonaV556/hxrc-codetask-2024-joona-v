using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// GameManager. Primarily handles the restart behavior when player dies
/// </summary>
public class GameManager : MonoBehaviour
{
    enum GameState
    {
        Playing,
        GameOver
    }

    GameState CurrentState = GameState.Playing;

    private void OnEnable()
    {
        DeathHandler.OnPlayerDied += EndGame;
        PlayerInput.OnResetTriggered += OnResetTriggered;
    }

    private void OnDisable()
    {
        DeathHandler.OnPlayerDied -= EndGame;
        PlayerInput.OnResetTriggered -= OnResetTriggered;
    }

    void EndGame()
    {
        CurrentState = GameState.GameOver;
    }

    // Allow restart only when game is over
    void OnResetTriggered()
    {
        if (CurrentState == GameState.GameOver) RestartGame();
    }

    void RestartGame()
    {
        var currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName, LoadSceneMode.Single);
    }
}
