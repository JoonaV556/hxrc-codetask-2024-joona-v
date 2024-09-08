using UnityEngine;

/// <summary>
/// Base class for consumables
/// </summary>
public class ConsumableBase : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        OnConsumed();
    }

    protected virtual void OnConsumed()
    {
        Debug.Log("I got consumed!");
    }
}
