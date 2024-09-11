using System;
using UnityEngine;

/// <summary>
/// Reads input and provides readable events for game events.
/// </summary>
public class PlayerInput : MonoBehaviour
{
    /*
        I mainly use native C# actions for game logic events, because code references can be easily tracked with basically any modern IDE

        UnityEvents are fine, but if they are referenced in inspector, finding references can be really hard
    */

    public static Action OnJumpTriggered; // Single event for when jump is triggered, allows triggering jump with multiple different keybinds
    public static Action OnResetTriggered;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpTriggered?.Invoke();
            OnResetTriggered?.Invoke(); // Reset is done with same keys as jump
        }
    }
}
