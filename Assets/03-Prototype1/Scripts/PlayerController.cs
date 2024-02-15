using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Changed in Scipt")]
    public Rigidbody playerRB;

    [Header("Set in Editor")]
    public float moveSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene("Main-Prototype 1");
        }
    }
}