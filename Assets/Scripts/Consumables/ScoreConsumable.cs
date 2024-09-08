using System;
using UnityEngine;

/// <summary>
/// Consumable which increases score
/// </summary>
public class ScoreConsumable : ConsumableBase
{
    public static Action OnScoreConsumableConsumed; // :DD
    protected override void OnConsumed()
    {
        Debug.Log("Score consumed!");
        OnScoreConsumableConsumed?.Invoke();
    }
}
