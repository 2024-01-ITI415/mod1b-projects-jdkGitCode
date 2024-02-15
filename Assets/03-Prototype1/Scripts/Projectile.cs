using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifespan;

    // Update is called once per frame
    void FixedUpdate()
    {
        lifespan -= Time.deltaTime;

        if (lifespan < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController eControl = other.GetComponent<EnemyController>();
            eControl.HitByProjectile();
            Destroy(this.gameObject);
        }
    }
}
