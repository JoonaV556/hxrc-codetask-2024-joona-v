using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spins object around in 2d (around z axis)
/// </summary>
public class Spinner : MonoBehaviour
{
    public float SpinSpeed = 1f;
    public bool RandomizeDirection = true;

    private void Start()
    {
        // Randomize w/ 50/50 chance
        var random = Random.Range(0f, 1f);
        if (random > 0.5f)
        {
            SpinSpeed *= -1;
        }
    }

    void Update()
    {
        // sync speed to framerate
        float actualSpeed = SpinSpeed * Time.deltaTime;
        // spin
        transform.Rotate(new Vector3(0f, 0f, actualSpeed), Space.Self);
    }
}
