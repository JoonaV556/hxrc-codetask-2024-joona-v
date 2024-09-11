using UnityEngine;

/// <summary>
/// Optional component which can be added to spawned objects for extra features
/// </summary>
public class SpawnableExtraInfo : MonoBehaviour
{
    /// <summary>
    /// How much extra distance is added between this object and other spawned objects
    /// </summary>
    public float ExtraGap = 0f;
}
