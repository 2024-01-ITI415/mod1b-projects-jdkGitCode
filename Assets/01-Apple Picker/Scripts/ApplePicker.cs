using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ApplePicker : MonoBehaviour
{
    [Header("Set in Inspector")]

    [SerializeField] GameObject basketPrefab;
    [SerializeField] int basketCount = 3;
    [SerializeField] float basketBottomY = -14f;
    [SerializeField] float basketSpacingY = 2f;

    [SerializeField] TextMeshProUGUI scoreText;
    private int scoreCount;

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

        scoreText.text = "Score: 0";
    }

    public void AppleMissed()
    {
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject tApple in tAppleArray)
        {
            Destroy(tApple);
        }
    }

    public void AddScore(int amount)
    {
        scoreCount += amount;
        scoreText.text = "Score: " + scoreCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
