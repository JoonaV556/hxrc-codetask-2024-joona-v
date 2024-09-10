using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    /*
        Spawns x amount of objects above player when game starts
        Each time player exceeds height of spawned object, new object is spawned
    */

    [Header("Spawnables")]
    public GameObject[] ObstaclePool; // Scalable pool for various kinds of obstacles
    public GameObject Star;
    public GameObject ColorChanger;

    [Space(20)]
    [Header("Params")]
    public float DistanceBetweenSpawnedObjects = 8f; // distance between each spawned object
    public int ObjectsSpawnedOnGameStart = 4;
    public Transform PlayerTransform; // Required for tracking when new objects must be spawned

    float lastSpawnY = 0f;
    SpawnableType lastSpawnedType = SpawnableType.none;

    enum SpawnableType
    {
        Obstacle = 0,
        Star = 1,
        ColorChanger = 2,
        none = 3,
    }

    #region SpawnMemory
    // Stores lastly spawned objects in memory
    // Required for:
    //  Tracking when new objects should be spawned
    //  Establishing specific spawning rules (prevent multiple color changers in row etc.)
    List<KeyValuePair<float, SpawnableType>> SpawnedObjectMemory;
    int memoryLength; // How many objects are stored in memory

    /// <summary>
    /// Adds new object to memory and automatically removes the last one if adding exceeds memory limit
    /// </summary>
    /// <param name="spawnHeight"></param>
    /// <param name="type"></param>
    void AddToMemory(float spawnHeight, SpawnableType type)
    {
        // Remove last object if exceeding limit 
        if (SpawnedObjectMemory.Count() == memoryLength)
        {
            SpawnedObjectMemory.Remove(SpawnedObjectMemory[0]);
        }
        SpawnedObjectMemory.Add(new KeyValuePair<float, SpawnableType>(spawnHeight, type));
    }
    #endregion

    /// <summary>
    /// Checks if player has jumped above last object in memory
    /// </summary>
    /// <returns>returns true</returns>
    bool ShouldSpawnNextObject()
    {
        // Return if no objects spawned yet 
        if (SpawnedObjectMemory == null || SpawnedObjectMemory.Count() == 0)
        {
            return false;
        }

        float playerHeight = PlayerTransform.position.y;
        if (playerHeight > SpawnedObjectMemory[0].Key) // Last object in memory is the first member on in the list, newest one is the last member
        {
            return true;
        }
        return false;
    }

    private void Update()
    {
        // Spawn next object when player height exceeds height of last spawned in the memory 
        if (ShouldSpawnNextObject())
        {
            SpawnNextObject();
        }
    }

    private void Start()
    {
        // init memory
        memoryLength = ObjectsSpawnedOnGameStart;
        SpawnedObjectMemory = new();

        // Spawn x amount of objects above player at start
        // Always spawn star first
        for (int i = 0; i < ObjectsSpawnedOnGameStart; i++)
        {
            if (i == 0)
            {
                SpawnNextObject(SpawnableType.Star);
                continue;
            }
            SpawnNextObject();
        }
    }

    /// <summary>
    /// Spawns next object.
    /// If override is not given, spawns random object.
    /// </summary>
    /// <param name="typeOverride"></param>
    void SpawnNextObject(SpawnableType typeOverride = SpawnableType.none)
    {
        KeyValuePair<GameObject, SpawnableType> objectToSpawn;

        // Get next object
        if (typeOverride == SpawnableType.none)
        {
            objectToSpawn = GetRandomObject(); // if override not given, get random object
        }
        else
        {
            // override set, get specific type of object
            objectToSpawn = new KeyValuePair<GameObject, SpawnableType>(
                GetSpawnableObject(typeOverride),
                typeOverride
                );
        }

        // Place object certain distance above last object
        float spawnHeight = lastSpawnY + DistanceBetweenSpawnedObjects;
        objectToSpawn.Key.GetComponent<Transform>().localPosition = new Vector3(
            0f,
            spawnHeight,
            0f
        );

        // Track spawn height
        lastSpawnY = spawnHeight;

        // Add spawned object to memory
        AddToMemory(spawnHeight, objectToSpawn.Value);
    }

    /// <summary>
    /// instantiates and returns random object from object pool
    /// </summary>
    /// <returns></returns>
    KeyValuePair<GameObject, SpawnableType> GetRandomObject()
    {
        // decide which type of object to spawn
        SpawnableType type = GetRandomType();

        GameObject obj = GetSpawnableObject(type);

        return new KeyValuePair<GameObject, SpawnableType>(obj, type);
    }

    /// <summary>
    /// Returns object based on given type.
    /// If type is obstacle, returns randomized obstacle
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    GameObject GetSpawnableObject(SpawnableType type)
    {
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
            random = UnityEngine.Random.Range(0, 2); // exclude color changer
        }
        else
        {
            random = UnityEngine.Random.Range(0, 3); // any type of object
        }
        return (SpawnableType)random;
    }

    GameObject GetRandomObstacle()
    {
        int obstacleCount = ObstaclePool.Count();
        int random = UnityEngine.Random.Range(0, obstacleCount - 1);
        return Instantiate(ObstaclePool[random]);
    }
}
