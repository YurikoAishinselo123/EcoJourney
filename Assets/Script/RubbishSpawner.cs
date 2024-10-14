using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbishSpawner : MonoBehaviour
{
    public GameObject objectPrefab;      // The rubbish prefab to spawn
    public Vector3[] spawnPositions;     // List of predefined spawn positions
    public int spawnLimit = 2;           // Maximum number of rubbish objects
    private List<GameObject> activeObjects = new List<GameObject>();  // List to track active rubbish objects

    void Update()
    {
        // Check if we are under the spawn limit
        if (activeObjects.Count < spawnLimit)
        {
            SpawnObject();
        }

        // Remove any destroyed rubbish from the list
        activeObjects.RemoveAll(item => item == null);
    }

    // Method to spawn a new object at a random position
    void SpawnObject()
    {
        // Randomly select a spawn position
        int randomIndex = Random.Range(0, spawnPositions.Length);

        // Instantiate the rubbish object
        GameObject newObj = Instantiate(objectPrefab, spawnPositions[randomIndex], Quaternion.identity);

        // Add the new object to the list of active rubbish objects
        activeObjects.Add(newObj);
    }

    // Method to remove a specific rubbish object and destroy it
    public void RemoveObject(GameObject obj)
    {
        if (activeObjects.Contains(obj))
        {
            activeObjects.Remove(obj);    // Remove from active objects list
            Destroy(obj);                 // Destroy the object
        }
    }
}
