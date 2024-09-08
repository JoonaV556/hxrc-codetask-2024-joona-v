using UnityEngine;
/// <summary>
/// Controls player movement
/// </summary>
public class PlayerMovementController : MonoBehaviour
{
    public Rigidbody2D PlayerRigidBody;
    public float JumpForce = 10f;
    public float MaxVelocity = 5f;
    bool JumpPending = false;

    private void OnEnable()
    {
        PlayerInput.OnJumpTriggered += Jump; // Sub to input events
    }

    private void OnDisable()
    {
        PlayerInput.OnJumpTriggered -= Jump;// Unsub from input events
    }

    // Input events might not happen at the same time as fixedUpdate which is used to control physics movement. 
    // Because of this we need to wait for jumps to be "consumed" in fixedupdate
    void Jump()
    {
        JumpPending = true;
    }

    private void FixedUpdate()
    {
        // Consume pending jump
        if (JumpPending)
        {
            // Calculate jump force
            Vector2 Force = transform.up.normalized * JumpForce;
            PlayerRigidBody.AddForce(Force, ForceMode2D.Impulse);
            JumpPending = false;
        }

        // Limit velocity so player cant go crazy speeds
        Vector2 Velocity = PlayerRigidBody.velocity;
        if (Velocity.magnitude > MaxVelocity)
        {
            PlayerRigidBody.velocity = Velocity.normalized * MaxVelocity;
        }
    }
}
