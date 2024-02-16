using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [Header("Changed in Scipt")]
    public Collider nonSpawnArea;
    private List<GameObject> enemyList = new();

    [Header("Set in Editor")]
    public GameObject enemyContainer;
    public GameObject enemyPrefab;
    public float spawnIntervalInSeconds = 5f;

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
        enemyList.Add(tEnemy);
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

        //Reinvokes the SpawnEnemy method to spawn the next one
        Invoke(nameof(SpawnEnemy), spawnIntervalInSeconds);
    }

    public void PurgeEnemies()
    {
        for (int i = 0;i<enemyList.Count;i++)
        {
            GameObject tEnemy = enemyList[i];
            enemyList.RemoveAt(i);
            Destroy(tEnemy);
        }
    }
}
