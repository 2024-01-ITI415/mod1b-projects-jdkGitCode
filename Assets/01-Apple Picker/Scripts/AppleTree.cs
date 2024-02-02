using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]

    [SerializeField] GameObject applePrefab;

    [SerializeField] float speed = 1f;

    [SerializeField] float edgeX = 10f;

    [SerializeField] float directionChangeChance = 0.1f;

    [SerializeField] float secondsBetweenAppleDrop = 1f;


    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DropApple), 2f);
    }

    void DropApple()
    {
        GameObject apple = Instantiate<GameObject>(applePrefab);
        apple.transform.position = transform.position;
        Invoke(nameof(DropApple), secondsBetweenAppleDrop);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        if (Mathf.Abs(pos.x) > edgeX)
        {
            speed = -speed;
        }
    }

    private void FixedUpdate()
    {
        if (Random.value < directionChangeChance) 
        {
            speed = -speed;
        }
    }
}
