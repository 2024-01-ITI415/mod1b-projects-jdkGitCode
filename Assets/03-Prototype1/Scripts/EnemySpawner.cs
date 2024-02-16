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
    public float enemyMoveSpeed = 3;

    // Start is called before the first frame update
    void Start()
    {
        //Gets the no spawning area collider and invokes SpawnEnemy
        nonSpawnArea = GameObject.Find("Non-Spawn Area").GetComponent<Collider>();
        Invoke(nameof(SpawnEnemy), 1f);
    }

    // Update is called once per frame
    void SpawnEnemy()
    {
        //Creates an enemy and sets its position to a random spot within a radius of the player
        GameObject tEnemy = Instantiate<GameObject>(enemyPrefab);
        tEnemy.transform.parent = enemyContainer.transform;

        float spawnPositionX = Random.Range(-33, 33);
        float spawnPositionZ = Random.Range(-33, 33);
        Vector3 spawnPosition = new(spawnPositionX, 1.1f, spawnPositionZ);

        //attempt count for debugging so it doesn't create an infinite loop
        int attemptCount = 0;
        //If the random spot is too close to the player it tries to pick a new random spot
        while (nonSpawnArea.bounds.Contains(spawnPosition))
        {
            spawnPositionX = Random.Range(-33, 33);
            spawnPositionZ = Random.Range(-33, 33);
            spawnPosition = new(spawnPositionX, 1.1f, spawnPositionZ);

            //Too many invalid spots breaks the loop
            attemptCount++;
            if (attemptCount > 100)
            {
                //Debug.Log("Too many spawn attempts");
                break;
            }
        }
        spawnPosition.y = 1.1f;
        tEnemy.transform.position = spawnPosition;

        tEnemy.GetComponent<EnemyController>().moveSpeed = enemyMoveSpeed;

        //Reinvokes the SpawnEnemy method to spawn the next one
        Invoke(nameof(SpawnEnemy), spawnIntervalInSeconds);
    }

    //Function for when the player gets hit to kill all active enemies
    public void PurgeEnemies()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemyList)
        {
            Destroy(enemy);
        }
    }
}
