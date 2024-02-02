using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePicker : MonoBehaviour
{
    [Header("Set in Inspector")]

    [SerializeField] GameObject basketPrefab;
    [SerializeField] int basketCount = 3;
    [SerializeField] float basketBottomY = -14f;
    [SerializeField] float basketSpacingY = 2f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < basketCount; i++)
        {
            GameObject tbasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tbasketGO.transform.position = pos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
