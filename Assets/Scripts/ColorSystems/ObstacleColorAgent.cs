using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleColorAgent : ColorAgent
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered");

        // If player entered trigger and colors dont match, trigger event on player
        if (other.gameObject.TryGetComponent(out PlayerColorAgent playerColorAgent))
        {
            Debug.Log("other is player");

            if (playerColorAgent.Color != this.Color)
            {
                Debug.Log("player doesnt have same color, triggering event");
                playerColorAgent.OnCollidedWithWrongColor();
            }
        }
    }
}
