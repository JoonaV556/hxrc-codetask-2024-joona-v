using System;
using UnityEngine;

/// <summary>
/// Changes player color when player consumes it
/// </summary>
public class ColorChangerConsumable : ConsumableBase
{
    public static Action OnColorChangerConsumed;

    protected override void OnConsumed(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerColorAgent agent))
        {
            agent.NextColor();
            Debug.Log("switching player color");
            OnColorChangerConsumed?.Invoke();
            Destroy(gameObject);
        }
    }
}
