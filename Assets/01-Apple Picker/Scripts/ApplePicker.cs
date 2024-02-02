using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class ApplePicker : MonoBehaviour
{
    [Header("Set in Inspector")]

    public GameObject basketPrefab;
    public List<GameObject> basketList;
    public int numBaskets = 3;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;

    public TextMeshProUGUI scoreText;
    public int scoreCount;

    public AppleTree apTree;
    public float treeSpeedIncrement = .05f;
    public float maxTreeSpeed = 45f;
    public float appleDelayIncrement = .005f;
    public float minAppleDelay = .2f;


    // Start is called before the first frame update
    void Start()
    {
        basketList = new List<GameObject>();

        for (int i = 0; i < numBaskets; i++)
        {
            GameObject tbasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tbasketGO.transform.position = pos;
            basketList.Add(tbasketGO);
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

        int basketIndex = basketList.Count - 1;
        GameObject tBasketGO = basketList[basketIndex];

        basketList.RemoveAt(basketIndex);
        Destroy(tBasketGO);

        if (basketList.Count == 0)
        {
            SceneManager.LoadScene("Main-ApplePicker");
        }
    }

    public void AddScore(int amount)
    {
        scoreCount += amount;
        scoreText.text = "Score: " + scoreCount.ToString();

        if (scoreCount > HighScore.highScore)
        {
            HighScore.highScore = scoreCount;
        }

        if (apTree.speed < maxTreeSpeed)
        {
            apTree.speed += Mathf.Sign(apTree.speed) * treeSpeedIncrement;
        }
        if (apTree.secondsBetweenAppleDrop > minAppleDelay)
        {
            apTree.secondsBetweenAppleDrop -= appleDelayIncrement;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
