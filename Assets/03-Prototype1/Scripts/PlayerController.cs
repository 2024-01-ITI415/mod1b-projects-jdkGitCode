using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Changed in Scipt")]
    public Rigidbody playerRB;
    public float currentHealth;

    [Header("Set in Editor")]
    public float moveSpeed = 1f;
    public float maxHealth = 3;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Moves player based on axis inputs (arrow keys or wasd)
        Vector3 velocityT = Vector3.zero;

        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            velocityT.x += Input.GetAxis("Horizontal") * moveSpeed;

        }

        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0)
        {
            velocityT.z += Input.GetAxis("Vertical") * moveSpeed;

        }

        playerRB.velocity = velocityT;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //When the player collides with an enemy, for now it resets the scene until more UI is implemented
        if (collision.gameObject.CompareTag("Enemy"))
        {
            currentHealth--;
            PrototypeGame.instance.OnPlayerHit();
        }
    }
}