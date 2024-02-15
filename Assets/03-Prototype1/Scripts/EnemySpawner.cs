using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [Header("Changed in Scipt")]
    public Collider nonSpawnArea;

    [Header("Set in Editor")]
    public Transform playerTransform;
    public GameObject enemyPrefab;
    public float spawnIntervalInSeconds = 5f;
    public float spawnRadius = 30;
    // Start is called before the first frame update
    void Start()
    {
        nonSpawnArea = GameObject.Find("Non-Spawn Area").GetComponent<Collider>();

        Invoke(nameof(SpawnEnemy), 2f);
    }

    // Update is called once per frame
    void SpawnEnemy()
    {
        GameObject tEnemy = Instantiate<GameObject>(enemyPrefab);
        Rigidbody tEnemyRB = tEnemy.GetComponent<Rigidbody>();

        Vector3 spawnPosition = Random.insideUnitCircle * spawnRadius;
        while (nonSpawnArea.bounds.Contains(spawnPosition))
        {
            spawnPosition = Random.insideUnitCircle * spawnRadius;
        }
        spawnPosition.y = 1.1f;
        tEnemy.transform.position = spawnPosition;

        Invoke(nameof(SpawnEnemy), spawnIntervalInSeconds);
    }

    private void FixedUpdate()
    {
        transform.position = playerTransform.position;
    }
}
