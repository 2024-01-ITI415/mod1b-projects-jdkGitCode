using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI;

    [Header("Set in inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;
    public float cameraTimer = 12f;

    [Header("Set dynamically")]
    public float camZ;

    private void Awake()
    {
        camZ = this.transform.position.z;
    }

    private void FixedUpdate()
    {
        Vector3 destination;

        if (POI == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            destination = POI.transform.position;

            if (POI.tag == "Projectile")
            {
                //float cameraTimer = 10f;
                if (POI.GetComponent<Rigidbody>().IsSleeping() || cameraTimer <= 0)
                {
                    POI = null;
                    cameraTimer = 10;
                    return;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    if (cameraTimer <= 8)
                    {
                        POI = null;
                        cameraTimer = 10;
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
