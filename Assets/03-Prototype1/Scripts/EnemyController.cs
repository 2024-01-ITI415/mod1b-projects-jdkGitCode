using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Changed in Scipt")]
    public Transform playerTransform;
    public Transform enemyTransform;
    public Rigidbody enemyRB;
    public float enemyClass;

    [Header("Set in Editor")]
    public float moveSpeed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        enemyTransform = transform;
        enemyRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Gets direction towards player and moves towards it
        Vector3 direction = (playerTransform.position - enemyTransform.position);
        direction.Normalize();

        enemyRB.velocity = direction * moveSpeed;
    }

    public void HitByProjectile()
    {
        //When hit by a projectile will either die if class 0 or split into smaller class enemies
        if (enemyClass == 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            //this will spawn additional smaller enemies
        }

        //Spawns in xp collectible after enemy dies

    }
}
