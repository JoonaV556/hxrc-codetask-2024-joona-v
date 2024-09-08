using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D PlayerRigidBody;
    public float JumpForce = 10f;
    public float MaxVelocity = 5f;
    bool JumpPending = false;

    private void OnEnable()
    {
        PlayerInput.OnJumpTriggered += AddPendingJump;
    }

    private void OnDisable()
    {
        PlayerInput.OnJumpTriggered -= AddPendingJump;
    }

    void AddPendingJump()
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
