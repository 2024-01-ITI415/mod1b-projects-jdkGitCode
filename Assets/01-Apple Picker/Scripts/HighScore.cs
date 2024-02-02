using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    static public int highScore = 1000;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }

        PlayerPrefs.SetInt("HighScore", highScore);
    }

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI gt = GetComponent<TextMeshProUGUI>();
        gt.text = "High Score: " + highScore;

        if (highScore > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
}
