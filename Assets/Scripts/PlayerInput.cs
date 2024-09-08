using System;
using UnityEngine;

/// <summary>
/// Reads input and provides readable events for game events.
/// </summary>
public class PlayerInput : MonoBehaviour
{
    public static Action OnJumpTriggered; // Single event for when jump is triggered, allows triggering jump with multiple different keybinds

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnJumpTriggered?.Invoke();
        }
    }
}
