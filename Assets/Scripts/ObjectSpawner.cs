using System.Linq;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawnables")]
    public GameObject[] ObstaclePool; // Scalable pool for various kinds of obstacles
    public GameObject Star;
    public GameObject ColorChanger;

    [Space(20)]
    [Header("Params")]
    public float DistanceBetweenSpawnedObjects = 8f; // distance between each spawned object
    public float PlayerMoveUpTreshold = 8f; // how much player has to move up to trigger new spawn wave
    public Transform PlayerTransform;

    float lastSpawnY = 0f;
    SpawnableType lastSpawnedType = SpawnableType.none;

    enum SpawnableType
    {
        Obstacle = 0,
        Star = 1,
        ColorChanger = 2,
        none = 3,
    }

    private void Start()
    {
        // Spawn first object above player
        SpawnNextObject();
    }

    void SpawnNextObject()
    {
        // Get next object
        GameObject obj = GetRandomObject();

        // Place object certain distance above last object
        obj.GetComponent<Transform>().localPosition = new Vector3(
            0f,
            lastSpawnY + DistanceBetweenSpawnedObjects,
            0f
        );
    }

    /// <summary>
    /// instantiates and returns random object from object pool
    /// </summary>
    /// <returns></returns>
    GameObject GetRandomObject()
    {
        // decide which type of object to spawn
        SpawnableType type = GetRandomType();

        GameObject obj = null;

        switch (type)
        {
            case SpawnableType.Obstacle:
                obj = GetRandomObstacle();
                break;
            case SpawnableType.Star:
                obj = Instantiate(Star);
                break;
            case SpawnableType.ColorChanger:
                obj = Instantiate(ColorChanger);
                break;
            case SpawnableType.none:
                obj = null; // GetRandomType wont return none
                break;
        }

        return obj;
    }

    /// <summary>
    /// returns random object type
    /// does not return two color changers in row
    /// </summary>
    /// <returns></returns>
    SpawnableType GetRandomType()
    {
        // get random int in possible object types
        int random;
        if (lastSpawnedType == SpawnableType.ColorChanger)
        {
            random = Random.Range(0, 1); // prevent two color changers in row
        }
        else
        {
            random = Random.Range(0, 2);
        }
        return (SpawnableType)random;
    }

    GameObject GetRandomObstacle()
    {
        int obstacleCount = ObstaclePool.Count();
        int random = Random.Range(0, obstacleCount - 1);
        return Instantiate(ObstaclePool[random]);
    }
}
