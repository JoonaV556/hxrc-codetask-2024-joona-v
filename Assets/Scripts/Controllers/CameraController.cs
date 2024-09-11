using UnityEngine;

/// <summary>
/// Controls camera movement. Makes camera follow player on y axis.
/// </summary>
public class CameraController : MonoBehaviour
{
    // Attach to camera gameobject

    public Transform PlayerTransform;

    private void Update()
    {
        // Get player pos
        float PlayerPositionY = PlayerTransform.position.y;

        // Follow player only if player is going up
        if (PlayerPositionY > transform.position.y)
        {
            transform.position = new Vector3(
            transform.position.x,
            PlayerPositionY, // We only modify the y position and keep others untouched
            transform.position.z
            );
        }
    }
}
