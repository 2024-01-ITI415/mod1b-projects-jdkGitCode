using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Set in Editor")]
    public GameObject projectileContainer;
    public GameObject projectilePrefab;
    public float fireRate = .5f;
    public float projectileSpeed = 2f;

    [Header("Changed in Scipt")]
    public float attackCooldown;
    public Vector3 mouseDeltaCheck;
    public Vector3 mousePosCheck;

    // Update is called once per frame
    void Update()
    {
        //Gets mouse position
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        mousePos3D.y = 1.1f;

        //Gets vector towards mouse position
        Vector3 mouseDelta = mousePos3D - transform.position;
        mouseDelta.Normalize();

        //debug variables
        mousePosCheck = mousePos3D;
        mouseDeltaCheck = mouseDelta;

        //Checks for left click and if attack is off cooldown
        if (Input.GetMouseButton(0) && attackCooldown <= 0)
        {
            //resets cooldown timer
            attackCooldown = fireRate;

            //creates projectile and gives it a velocity towards the mouse
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.transform.position = transform.position;
            projectile.transform.parent = projectileContainer.transform;

            Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();
            projectileRB.velocity = mouseDelta * projectileSpeed;
        }

        attackCooldown -= Time.deltaTime;
    }
}
