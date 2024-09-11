using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles removing old object spawned by spawner. for opitmisation.
/// </summary>
public class SpawnedObjectRemover : MonoBehaviour
{
    public float RemoveBelowPlayerDistanceTreshold = 20f;
    public Transform PlayerTransform;


    // Mainly handles obstacles since they are the only type of objects which will stay around after player has passed them
    private void OnEnable()
    {
        ObjectSpawner.OnObstacleSpawned += AddToPending; // Add spawned objects to be removed
    }

    private void OnDisable()
    {
        ObjectSpawner.OnObstacleSpawned -= AddToPending;
    }

    List<GameObject> PendingForRemoval;

    void AddToPending(GameObject spawnedObject)
    {
        if (PendingForRemoval == null)
        {
            PendingForRemoval = new(); // init list if not yet created
        }

        PendingForRemoval.Add(spawnedObject);
    }

    List<GameObject> ToRemove = new(); // objects to remove this frame
    private void Update()
    {
        // check old objects
        foreach (var obstacle in PendingForRemoval)
        {
            if (ShouldBeRemoved(obstacle))
            {
                ToRemove.Add(obstacle);
            }
        }

        // Remove old objects
        foreach (var obstacle in ToRemove)
        {
            PendingForRemoval.Remove(obstacle);
            Destroy(obstacle.gameObject);
        }

        // Clear toremove
        ToRemove.Clear();
    }

    bool ShouldBeRemoved(GameObject obj)
    {
        if (obj.transform.position.y < PlayerTransform.position.y - RemoveBelowPlayerDistanceTreshold)
        {
            return true;
        }
        return false;
    }
}
