using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroys spawned particlesystems
/// </summary>
public class ParticleKiller : MonoBehaviour
{
    ParticleSystem particle;

    private void OnEnable()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!particle.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
