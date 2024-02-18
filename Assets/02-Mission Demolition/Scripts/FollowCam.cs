using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI;
    public GameObject Debug_POI;
    public static GameObject slingshotView;

    [Header("Set in inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;
    public float maxCameraTimer = 12f;

    [Header("Set dynamically")]
    public float camZ;
    public float cameraTimer;

    private void Awake()
    {
        camZ = this.transform.position.z;
        slingshotView = GameObject.Find("ViewSlingshot");
        cameraTimer = maxCameraTimer;
    }

    private void FixedUpdate()
    {
        Debug_POI =  POI;

        Vector3 destination;

        if (POI == slingshotView)
        {
            destination = Vector3.zero;
        }
        else
        {
            destination = POI.transform.position;

            if (POI.CompareTag("Projectile"))
            {
                if (POI.GetComponent<Rigidbody>().IsSleeping() || cameraTimer <= 0)
                {
                    POI = slingshotView;
                    cameraTimer = maxCameraTimer;
                    return;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    if (cameraTimer <= maxCameraTimer - 2)
                    {
                        POI = slingshotView;
                        cameraTimer = maxCameraTimer;
                    }
                }
                    cameraTimer -= Time.deltaTime;
            }
        }

        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);

        destination = Vector3.Lerp(transform.position, destination, easing);

        destination.z = camZ;

        transform.position = destination;
        Camera.main.orthographicSize = destination.y + 10;
    }
}
