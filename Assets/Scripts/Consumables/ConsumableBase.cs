using UnityEngine;

/// <summary>
/// Base class for consumables
/// </summary>
public class ConsumableBase : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        OnConsumed(other);
    }

    protected virtual void OnConsumed(Collider2D other)
    {
        Debug.Log("I got consumed!");
    }
}
