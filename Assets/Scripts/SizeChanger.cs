using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Continuously changes scale of object from one scale to another
/// </summary>
public class SizeChanger : MonoBehaviour
{
    public float ScaleSmall, ScaleBig;

    public float ScaleChangePerSecond = 0.1f;

    public float directionMultiplier;

    private void Update()
    {
        // Decide which way to scale
        directionMultiplier = GetScaleDirectionMultiplier();

        // Sync to framerate & modify with direction
        float actualScaleChange = ScaleChangePerSecond * Time.deltaTime * directionMultiplier;

        // Modify size
        transform.localScale = new Vector3(
            transform.localScale.x + actualScaleChange,
            transform.localScale.y + actualScaleChange,
            transform.localScale.z + actualScaleChange
        );
    }

    /// <summary>
    /// Returns which way scaling should happen
    /// </summary>
    float directionMultiplierLastFrame = 1f;
    float GetScaleDirectionMultiplier()
    {
        if (transform.localScale.x > ScaleBig)
        {
            // Time to shrink down
            return directionMultiplierLastFrame = -1f;
        }
        else if (transform.localScale.x < ScaleSmall)
        {
            // Time to grow
            return directionMultiplierLastFrame = 1;
        }
        else
        {
            // Limits not reached, continue scaling same way as last frame
            return directionMultiplierLastFrame;
        }

    }
}
