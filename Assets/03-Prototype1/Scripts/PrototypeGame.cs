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
        UpdateHud();

        Invoke(nameof(IncreaseDefficulty), 1f);
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = "Timer: " + Time.time.ToString("#.00");
    }

    void IncreaseDefficulty()
    {
        if (enemySpawner.enemyMoveSpeed < 15)
        {
            enemySpawner.enemyMoveSpeed += .2f;
        }
        if (enemySpawner.spawnIntervalInSeconds > .5)
        {
            enemySpawner.spawnIntervalInSeconds -= .1f;
        }
        Invoke(nameof(IncreaseDefficulty), 3f);
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
        foreach (Button button in LevelUpButtons)
        {
            button.gameObject.SetActive(true);
        }
        Time.timeScale = 0;
    }
    public void IncreaseMoveSpeed()
    {
        if (playerController.moveSpeed < 20)
        {
            playerController.moveSpeed *= 1.1f;
            if (playerController.moveSpeed > 20)
            {
                LevelUpButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Movement Speed Maxed Out";
            }
        }
        FinishLevelUp();
    }
    public void IncreaseFireRate()
    {
        if (playerAttack.fireRate > .1)
        {
            playerAttack.fireRate *= .9f;
            if (playerController.moveSpeed < .1)
            {
                LevelUpButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "Fire Rate Maxed Out";
            }
        }
        FinishLevelUp();
    }
    public void FullyHeal()
    {
        playerController.currentHealth = playerController.maxHealth;
        FinishLevelUp();
    }
    private void FinishLevelUp()
    {
        UpdateHud();
        foreach (Button button in LevelUpButtons)
        {
            button.gameObject.SetActive(false);
        }
        Time.timeScale = 1;
    }

    void GameOver()
    {
        SceneManager.LoadScene("Main-Prototype 1");
    }
}
