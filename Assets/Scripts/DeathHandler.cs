using System;
using UnityEngine;

/// <summary>
/// Handles player death
/// </summary>
public class DeathHandler : MonoBehaviour
{
    public struct DeathInfo
    {
        public Vector2 position;
    }

    public static Action<DeathInfo> OnPlayerDied;

    // sub to various game events - Utilizing events to tie up game logic is more scalable than establishing hard references between components.
    // Refactoring is easier & less spaghetti
    private void OnEnable()
    {
        // Die when player hits wrong color
        PlayerColorAgent.OnCollidedWithWrongColorEvent += Die;
        // Die when player falls below lower camera edge
        OutOfScreenHandler.OnPlayerFellBelowCamera += Die;
    }

    // Unsub from events
    private void OnDisable()
    {
        PlayerColorAgent.OnCollidedWithWrongColorEvent -= Die;
        OutOfScreenHandler.OnPlayerFellBelowCamera -= Die;
    }

    public void Die()
    {
        // disable movement
        GetComponent<PlayerMovementController>().enabled = false; // disable movement controller
        Rigidbody2D playerRigidBody = GetComponent<Rigidbody2D>();
        playerRigidBody.velocity = Vector2.zero; // Stop ongoing physics movement
        playerRigidBody.isKinematic = true; // Prevent further physics movement

        // Disable visual 
        GetComponent<SpriteRenderer>().enabled = false;

        Debug.Log("died");
        OnPlayerDied?.Invoke(
            new DeathInfo
            {
                position = transform.position,
            }
        );
    }
}
