using System;
using UnityEngine;

/// <summary>
/// Consumable which increases score
/// </summary>
public class ScoreConsumable : ConsumableBase
{
    /// <summary>
    /// Fired when score consumable (star) is consumed. Vector2 is for star location at the moment of consuming (useful for visual stuff etc)
    /// </summary>
    public static Action<ConsumeInfo> OnScoreConsumableConsumed; // :DD
    protected override void OnConsumed()
    {
        Debug.Log("Score consumed!");

        // Fire event for vfx etc.
        var info = new ConsumeInfo
        {
            position = transform.position
        };
        OnScoreConsumableConsumed?.Invoke(info);

        // Destroy consumable object
        Destroy(gameObject);
    }
}

public struct ConsumeInfo
{
    /// <summary>
    /// Position of consumed consumable at the point of consuming
    /// </summary>
    public Vector2 position;
}
