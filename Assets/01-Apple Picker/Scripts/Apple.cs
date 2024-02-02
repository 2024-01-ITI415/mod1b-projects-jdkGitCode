using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public static float BottomY = -20f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < BottomY)
        {
            Destroy(gameObject);

            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
            apScript.AppleMissed();
        }
    }
}
