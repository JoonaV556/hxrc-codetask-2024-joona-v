using System;
using UnityEngine;

/// <summary>
/// Reacts to player falling below camera
/// </summary>
public class OutOfScreenHandler : MonoBehaviour
{
    public static Action OnPlayerFellBelowCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If other is player, fire event
        if (other.gameObject.CompareTag("Player"))
        {
            OnPlayerFellBelowCamera?.Invoke();
        }
    }
}
