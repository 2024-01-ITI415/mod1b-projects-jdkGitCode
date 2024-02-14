using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Set in Editor")]
    public Transform playerTransform;

    private Vector3 tempPos;

    // Update is called once per frame
    void Update()
    {
        tempPos = playerTransform.position;
        tempPos.y = transform.position.y;
        transform.position = tempPos;
    }
}
