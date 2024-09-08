using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Continuously changes scale of object from one scale to another
/// </summary>
public class SizeChanger : MonoBehaviour
{
    public float ScaleSmall, ScaleBig;

    public float ScaleChangePerSecond = 0.1f;

    [Tooltip("If true, object will start with a random scale between big and small. useful for creating randomness between similar objects")]
    public bool StartWithRandomScale = true;

    private void Start()
    {
        // Start with random scale 
        if (StartWithRandomScale)
        {
            float randomScale = UnityEngine.Random.Range(ScaleSmall, ScaleBig);
            transform.localScale = new Vector3(
            randomScale,
            randomScale,
            randomScale
            );
        }
    }

    private void Update()
    {
        // Decide which way to scale
        float directionMultiplier = GetScaleDirectionMultiplier();

        // Sync to framerate & modify with wanted direction
        float actualScaleChange = ScaleChangePerSecond * Time.deltaTime * directionMultiplier;

        // Modify size
        transform.localScale = new Vector3(
            transform.localScale.x + actualScaleChange,
            transform.localScale.y + actualScaleChange,
            transform.localScale.z + actualScaleChange
        );
    }

    float directionMultiplierLastFrame = 1f;
    /// <summary>
    /// Returns which way scaling should happen
    /// </summary>
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
