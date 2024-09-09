using System;
using UnityEngine;

/// <summary>
/// Handles player death
/// </summary>
public class DeathHandler : MonoBehaviour
{
    public static Action OnPlayerDied;

    // Reacts to different events which might cause death & does death stuff
    private void OnEnable()
    {
        // Die when player hits wrong color
        PlayerColorAgent.OnCollidedWithWrongColorEvent += Die;
    }

    private void OnDisable()
    {
        PlayerColorAgent.OnCollidedWithWrongColorEvent -= Die;
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
        OnPlayerDied?.Invoke();
    }

    // Cause death when player goes below camera height
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CameraBottom")) // Camera has a child collider attached to bottom of screen, which has the CameraBottom tag
        {
            Debug.Log("Hit camera bottom");
            Die();
        }
    }
}
