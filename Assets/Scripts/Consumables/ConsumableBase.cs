using UnityEngine;

/// <summary>
/// Base class for consumables
/// </summary>
public class ConsumableBase : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        OnConsumed();
    }

    protected virtual void OnConsumed()
    {
        Debug.Log("I got consumed!");
    }
}
