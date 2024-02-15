using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [Header("Changed in Scipt")]
    public Collider nonSpawnArea;

    [Header("Set in Editor")]
    public GameObject enemyContainer;
    public GameObject enemyPrefab;
    public float spawnIntervalInSeconds = 5f;
    public float spawnRadius = 30;
    // Start is called before the first frame update
    void Start()
    {
        //Gets the no spawning area collider and invokes SpawnEnemy
        nonSpawnArea = GameObject.Find("Non-Spawn Area").GetComponent<Collider>();
        Invoke(nameof(SpawnEnemy), 2f);
    }

    // Update is called once per frame
    void SpawnEnemy()
    {
        //Creates an enemy and sets its position to a random spot within a radius of the player
        GameObject tEnemy = Instantiate<GameObject>(enemyPrefab);
        tEnemy.transform.parent = enemyContainer.transform;
        Rigidbody tEnemyRB = tEnemy.GetComponent<Rigidbody>();

        Vector3 spawnPosition = Random.insideUnitCircle * spawnRadius;
        spawnPosition += transform.position;
        //Debug.Log("spawn pos should be " + spawnPosition);
        //attempt count for debugging so it doesn't create an infinite loop
        int attemptCount = 0;
        //If the random spot is too close to the player it tries to pick a new random spot
        while (nonSpawnArea.bounds.Contains(spawnPosition))
        {
            spawnPosition = Random.insideUnitCircle * spawnRadius;
            
            Debug.Log("shouldn't spawn here!");
            //Too many invalid spots breaks the loop
            attemptCount++;
            if (attemptCount > 30)
            {
                Debug.Log("Too many spawn attempts");
                break;
            }
        }
        spawnPosition.y = 1.1f;
        tEnemy.transform.position = spawnPosition;

        //Reinvokes the SpawnEnemy method to spawn the next one
        Invoke(nameof(SpawnEnemy), spawnIntervalInSeconds);
    }
}
