using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]

    public GameObject applePrefab;

    public float speed = 1f;

    public float edgeX = 10f;

    public float directionChangeChance = 0.1f;

    public float secondsBetweenAppleDrop = 1f;

    public List<Material> appleMaterials = new();

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DropApple), 2f);
    }

    void DropApple()
    {
        GameObject tApple = Instantiate<GameObject>(applePrefab);
        tApple.transform.position = transform.position;

        int randomInt = Random.Range(0, 3);
        Material tAppleMat = appleMaterials[randomInt];
        tApple.GetComponent<MeshRenderer>().material = tAppleMat;

        Invoke(nameof(DropApple), secondsBetweenAppleDrop);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        if (pos.x > edgeX)
        {
            speed = -Mathf.Abs(speed);
        }
        else if (pos.x < -edgeX)
        {
            speed = Mathf.Abs(speed);
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
