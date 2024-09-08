using UnityEngine;

/// <summary>
/// Handles player death
/// </summary>
public class DeathHandler : MonoBehaviour
{
    // Reacts to different events which might cause death & does death stuff
    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    public void Die()
    {
        Debug.Log("Died");

        // disable movement
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<PlayerMovementController>().enabled = false;

        // Disable visual 
        GetComponent<SpriteRenderer>().enabled = false;
    }

    // Cause death when player goes below camera height
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit something");
        if (collision.gameObject.CompareTag("CameraBottom")) // Camera has a child collider attached to bottom of screen, which has the CameraBottom tag
        {
            Debug.Log("Hit camera bottom");
            Die();
        }
    }
}
