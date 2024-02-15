using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrototypeGame : MonoBehaviour
{
    [Header("Changed in Scipt")]
    public float level;
    public float maxExp = 3;
    public float currentEXP;
    private float currentTime;

    [Header("Set in Editor")]
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI timerText;
    public float expScalingAmount = 3;

    // Start is called before the first frame update
    void Start()
    {
       UpdateHud();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        timerText.text = "Timer: " + currentTime;
    }

    void UpdateHud()
    {
        levelText.text = "Level: " + level;
        expText.text = "EXP: " + currentEXP + "/" + maxExp;
    }

    void LevelUp()
    {
        level++;
        currentEXP = 0;
        maxExp += expScalingAmount;
    }
}
