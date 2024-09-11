using UnityEngine;

/// <summary>
/// Detects when player hits obstacle with different color
/// </summary>
public class ObstacleColorAgent : ColorAgent
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If player entered trigger and colors dont match, trigger event on player
        if (other.gameObject.TryGetComponent(out PlayerColorAgent playerColorAgent))
        {
            if (playerColorAgent.Color != this.Color)
            {
                playerColorAgent.OnCollidedWithWrongColor();
            }
        }
    }
}
