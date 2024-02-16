using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PrototypeGame : MonoBehaviour
{
    static public PrototypeGame instance;

    [Header("Changed in Scipt")]
    public float level;
    static public float maxExp = 3;
    static public float currentEXP;
    private float currentTime;

    [Header("Set in Editor")]
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI timerText;
    public PlayerController playerController;
    public EnemySpawner enemySpawner;
    public float expScalingAmount = 3;

    private void Awake()
    {
        instance = this;
    }

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

    public void UpdateHud()
    {
        levelText.text = "Level: " + level;
        expText.text = "EXP: " + currentEXP + "/" + maxExp;
        healthText.text = "HP: " + playerController.currentHealth + "/" + playerController.maxHealth;

        
    }

    public void OnPlayerHit()
    {
        enemySpawner.PurgeEnemies();
        if (playerController.currentHealth <= 0)
        {
            GameOver();
        }
        UpdateHud();
    }

    public void IncrementEXP()
    {
        //Increaases xp by 1, tries to level up and then updates the hud
        currentEXP++;
        if (currentEXP >= maxExp) LevelUp();
        UpdateHud();
    }

    void LevelUp()
    {
        level++;
        currentEXP -= maxExp;
        maxExp += expScalingAmount;
    }

    void GameOver()
    {
        SceneManager.LoadScene("Main-Prototype 1");
    }
}
