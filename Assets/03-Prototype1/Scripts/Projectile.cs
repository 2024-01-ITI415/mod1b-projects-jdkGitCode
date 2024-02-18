using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifespan;

    // Update is called once per frame
    void FixedUpdate()
    {
        //Counts down lifespan
        lifespan -= Time.deltaTime;

        //At 0 lifespan kills the projectile
        if (lifespan < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //When it hits an enemy the projectile kills itself and triggers enemy's on hit script
        if (other.CompareTag("Enemy"))
        {
            EnemyController eControl = other.GetComponent<EnemyController>();
            eControl.HitByProjectile();
            Destroy(this.gameObject);
        }
    }
}
