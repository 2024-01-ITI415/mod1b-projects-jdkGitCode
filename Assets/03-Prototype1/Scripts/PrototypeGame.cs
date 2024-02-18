using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrototypeGame : MonoBehaviour
{
    static public PrototypeGame instance;

    [Header("Changed in Scipt")]
    public float level;
    static public float maxExp = 3;
    static public float currentEXP;
    private float currentTime;
    private PlayerController playerController;
    private Attack playerAttack;

    [Header("Set in Editor")]
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI timerText;
    public List<Button> LevelUpButtons = new();
    public GameObject gameOverUI;
    public GameObject player;
    public EnemySpawner enemySpawner;
    public float expScalingAmount = 3;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        playerAttack = player.GetComponent<Attack>();
        foreach (Button button in LevelUpButtons)
        {
            button.gameObject.SetActive(false);
        }
        gameOverUI.SetActive(false);
        UpdateHud();

        Invoke(nameof(IncreaseDefficulty), 1f);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        timerText.text = "Timer: " + currentTime.ToString("#.00");
    }

    [Header("Difficulty Scaling")]
    public float enemyMoveSpeedScaling = .1f;
    public float enemySpawnRateScaling = .1f;
    public float difficultyScalingIntervalInSeconds = 3;
    //Slightly increases enemy stats and then reinvokes function in 3 seconds
    void IncreaseDefficulty()
    {
        if (enemySpawner.moddedEnemyMoveSpeed < 15)
        {
            enemySpawner.moddedEnemyMoveSpeed += enemyMoveSpeedScaling;
        }
        if (enemySpawner.moddedSpawnInterval > .5)
        {
            enemySpawner.moddedSpawnInterval -= enemySpawnRateScaling;
        }
        Invoke(nameof(IncreaseDefficulty), difficultyScalingIntervalInSeconds);
    }

    //Updates all of the HUD text
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

    [Header("Level Up Multipliers")]
    public float playerMoveSpeedMulti = 1.1f;
    public float playerFireRateMulti = .9f;
    void LevelUp()
    {
        //Increases Level and resets exp
        level++;
        currentEXP -= maxExp;
        maxExp += expScalingAmount;

        //Activates the levelUp buttons and pauses the game
        foreach (Button button in LevelUpButtons)
        {
            button.gameObject.SetActive(true);
        }
        Time.timeScale = 0;
    }

    //Button funciton for movement speed increase
    public void IncreaseMoveSpeed()
    {
        if (playerController.moddedMoveSpeed < 20)
        {
            playerController.moddedMoveSpeed *= playerMoveSpeedMulti;
            if (playerController.moddedMoveSpeed > 20)
            {
                LevelUpButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Movement Speed Maxed Out";
            }
        }
        FinishLevelUp();
    }

    //Button funciton for fire rate increase
    public void IncreaseFireRate()
    {
        if (playerAttack.moddedFireRate > .1)
        {
            playerAttack.moddedFireRate *= playerFireRateMulti;
            if (playerAttack.moddedFireRate < .1)
            {
                LevelUpButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "Fire Rate Maxed Out";
            }
        }
        FinishLevelUp();
    }

    //Button function for full heal
    public void FullyHeal()
    {
        playerController.currentHealth = playerController.maxHealth;
        FinishLevelUp();
    }

    //Updates HUD, deactivates buttons, and unpauses game
    private void FinishLevelUp()
    {
        UpdateHud();
        foreach (Button button in LevelUpButtons)
        {
            button.gameObject.SetActive(false);
        }
        Time.timeScale = 1;
    }

    //Freezes time and activates game over menu
    void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
        maxExp = 3;
        currentEXP = 0;
    }

    //Restarts the scene
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main-Prototype 1");
    }

    //Loads the main menu scene
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SceneMain");
    }
}
