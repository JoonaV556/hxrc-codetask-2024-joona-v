using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Infinite spawner which spawns objects above player
/// </summary>
public class ObjectSpawner : MonoBehaviour
{
    /*
        Handles infinite spawning of objects in the game.
        
        Spawns x amount of objects above player when game starts
        Each time player exceeds height of spawned object, new object is spawned

        prevents two stars in row
        prevents two color changers in row


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

    /// <summary>
    /// Fired when obstacle is spawned - used for passing spawned objects for ObjectRemover to be removed once they are certain height below player
    /// </summary>
    public static Action<GameObject> OnObstacleSpawned;

    float lastSpawnY = 0f;
    float lastObjectExtraDistance = 0f; // if last object had extra distance, this is used for the next object
    SpawnableType lastSpawnedType = SpawnableType.none;

    // Each time new object is spawned, its type is selected using these possible types
    enum SpawnableType
    {
        Obstacle = 0,
        Star = 1,
        ColorChanger = 2,
        none = 3, // none is only used as a default value for some properties at game start. Not actually used when spawning new obejcts
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
    /// Checks if player has jumped above last object in memory.
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
        // init memory - we keep memory as long as obejcts spawned on game start
        memoryLength = ObjectsSpawnedOnGameStart;
        SpawnedObjectMemory = new();

        // Spawn x amount of objects above player at start
        for (int i = 0; i < ObjectsSpawnedOnGameStart; i++)
        {
            if (i == 0)
            {
                SpawnNextObject(SpawnableType.Star); // Always spawn star first
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

        // Decide what type of object to spawn
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

        // check if object has extra rules for spawning
        float extraGap = 0f; // optional extra distance added between this and other objects
        if (objectToSpawn.Key.TryGetComponent(out SpawnableExtraInfo info))
        {
            extraGap = info.ExtraGap; // if spawned object has optional extra info, get extra gap
        }

        // Place object certain distance above last spawned object
        float spawnHeight = lastSpawnY + DistanceBetweenSpawnedObjects + extraGap + lastObjectExtraDistance;
        lastObjectExtraDistance = extraGap;
        extraGap = 0f; // zero extra gap for next spawn. its not needed anymore
        objectToSpawn.Key.GetComponent<Transform>().localPosition = new Vector3(
            0f,
            spawnHeight,
            0f
        );

        // Track spawn height
        lastSpawnY = spawnHeight;

        // Track object type
        lastSpawnedType = objectToSpawn.Value;

        // if object was obstacle - fire event
        if (objectToSpawn.Value == SpawnableType.Obstacle)
        {
            OnObstacleSpawned?.Invoke(objectToSpawn.Key);
        }

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
                obj = null; // not used when spawning new object, read comment at enum declaration
                break;
        }
        return obj;
    }

    /// <summary>
    /// returns random object type w/ some custom rules. this is the heart of the spawner alglorithm
    /// </summary>
    /// <returns></returns>
    SpawnableType GetRandomType()
    {
        // Always spawn atleast 1 obstacle between each 3 spawns
        if (!ObstaclesSpawnedInLastThree())
        {
            return SpawnableType.Obstacle;
        }

        // No need to spawn obstacle now, decide random type \/ \/

        // filter out certain objects with some prevention rules 
        List<int> possibleTypes = new List<int>
        {
            0, // obstacle
            1, // star 
            2  // color changer
        };

        // prevent two color changers in row
        if (lastSpawnedType == SpawnableType.ColorChanger)
        {
            possibleTypes.Remove(2); // Remove color changer
        }

        // Prevent 3 obstacles in row
        if (WereLastTwoObstacles())
        {
            possibleTypes.Remove(0); // remove obstacle
        }

        // return random type after unwanted types have been excluded
        string types = "possible types: ";
        foreach (var type in possibleTypes)
        {
            types += type.ToString();
        }

        // Cast randomized integer to SpawnableType - see enum declaration at top for details
        return (SpawnableType)GetRandomInt(possibleTypes);
    }

    /// <summary>
    /// Returns random member from list of ints
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    int GetRandomInt(List<int> list)
    {
        if (list.Count() == 1)
        {
            return list[0];
        }
        int random = UnityEngine.Random.Range(0, list.Count());
        return list[random];
    }

    /// <summary>
    /// Checks whether any osbtacles were spawned during last three spawns
    /// </summary>
    /// <returns></returns>
    bool ObstaclesSpawnedInLastThree()
    {
        bool obstacleSpawnedInLastThree = false;
        foreach (var spawnedObject in SpawnedObjectMemory)
        {
            if (spawnedObject.Value == SpawnableType.Obstacle)
            {
                obstacleSpawnedInLastThree = true;

            }
        }
        return obstacleSpawnedInLastThree;
    }

    bool WereLastTwoObstacles()
    {
        if (SpawnedObjectMemory == null || SpawnedObjectMemory.Count() < 2)
        {
            return false;
        }
        int obstacles = 0;
        int lastIndex = SpawnedObjectMemory.Count() - 1;
        for (int i = lastIndex; i > lastIndex - 2; i--)
        {
            if (SpawnedObjectMemory[i].Value == SpawnableType.Obstacle)
            {
                obstacles++;
            }
        }
        return obstacles >= 2;
    }

    GameObject GetRandomObstacle()
    {
        int obstacleCount = ObstaclePool.Count();
        int random = UnityEngine.Random.Range(0, obstacleCount - 1);
        return Instantiate(ObstaclePool[random]);
    }
}
