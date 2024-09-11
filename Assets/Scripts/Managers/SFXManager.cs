using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource GameOverSFX;
    public AudioSource ScoreIncreaseSFX;
    public AudioSource ColorSwitchedSFX;
    public AudioSource JumpSFX;

    private void OnEnable()
    {
        GameManager.OnGameOver += PlayGameOverSFX;
        ScoreTracker.OnScoreIncreased += PlayScoreSFX;
        PlayerMovementController.OnJumpedEvent += PlayJumpSFX;
        ColorChangerConsumable.OnColorChangerConsumed += PlayColorSwitchSFX;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= PlayGameOverSFX;
        ScoreTracker.OnScoreIncreased -= PlayScoreSFX;
        PlayerMovementController.OnJumpedEvent -= PlayJumpSFX;
        ColorChangerConsumable.OnColorChangerConsumed -= PlayColorSwitchSFX;
    }

    void PlayGameOverSFX()
    {
        GameOverSFX.Play();
    }

    void PlayScoreSFX(int newScore)
    {
        ScoreIncreaseSFX.Play();
    }

    void PlayColorSwitchSFX()
    {
        ColorSwitchedSFX.Play();
    }

    void PlayJumpSFX()
    {
        JumpSFX.Play();
    }
}
