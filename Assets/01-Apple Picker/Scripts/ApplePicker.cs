using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class ApplePicker : MonoBehaviour
{
    [Header("Set in Inspector")]

    [SerializeField] GameObject basketPrefab;
    public List<GameObject> basketList;
    [SerializeField] int numBaskets = 3;
    [SerializeField] float basketBottomY = -14f;
    [SerializeField] float basketSpacingY = 2f;

    [SerializeField] TextMeshProUGUI scoreText;
    private int scoreCount;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
