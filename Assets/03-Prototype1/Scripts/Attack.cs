using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Set in Editor")]
    public GameObject projectilePrefab;
    public float attackRate = .5f;
    public float projectileSpeed = 2f;

    [Header("Changed in Scipt")]
    public float attackCooldown;
    public Vector3 mouseDeltaCheck;
    public Vector3 mousePosCheck;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        mousePos3D.y = 1.1f;

        Vector3 mouseDelta = mousePos3D - transform.position;
        mouseDelta.Normalize();

        mousePosCheck = mousePos3D;
        mouseDeltaCheck = mouseDelta;


        if (Input.GetMouseButtonDown(0) && attackCooldown <= 0)
        {
            attackCooldown = attackRate;

            GameObject projectile = Instantiate(projectilePrefab);
            projectile.transform.position = transform.position;

            Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();

            projectileRB.velocity = mouseDelta * projectileSpeed;
        }

        attackCooldown -= Time.deltaTime;
    }
}
