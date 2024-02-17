using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : ALevel
{
    [SerializeField] private GameObject UI;

    Dictionary<int, levelCharacteristics> levelStats = new Dictionary<int, levelCharacteristics>()
    {
        {1, new levelCharacteristics(){enemySpawnerNumber = 3, chestNumber = 5, enemyCoefficient = 2} }, 
        {2, new levelCharacteristics(){enemySpawnerNumber = 6, chestNumber = 10, enemyCoefficient = 3} },
        {3, new levelCharacteristics(){enemySpawnerNumber = 9, chestNumber = 12, enemyCoefficient = 4.5f} }
    };

    [SerializeField] private GameObject angryBoss;
    [SerializeField] private GameObject lazinessBoss;
    [SerializeField] private GameObject fearBoss;

    Dictionary<string, GameObject> levelBoss;

    [SerializeField] private GameObject angryEnemy;
    [SerializeField] private GameObject blob;
    [SerializeField] private GameObject fearEnemy;

    Dictionary<string, GameObject> levelEnemy;

    struct levelCharacteristics
    {
        public int enemySpawnerNumber;
        public int chestNumber;
        public float enemyCoefficient;
    }

    void Awake()
    {
        levelEnemy = new Dictionary<string, GameObject>()
        {
            { "AngerLevel", angryEnemy },
            { "LazinessLevel", blob },
            { "FearLevel", fearEnemy }
        };
        levelBoss = new Dictionary<string, GameObject>()
        {
            { "AngerLevel", angryBoss },
            { "LazinessLevel", lazinessBoss },
            { "FearLevel", fearBoss }
        };

        Time.timeScale = 1;
        UI = GameObject.FindGameObjectWithTag("UI");
        UI.SetActive(true);

        NavigateMenuScript.currentLevel ??= SceneManager.GetActiveScene().name;
        
        generateBlocks();
        Instantiate(Player, new Vector2(100 / 2, 60), Quaternion.identity);
        
        GetComponent<LightSurfaceSpawner>().SpawnLights(10);
        GetComponent<EnemySpawnerCreation>().enemy = levelEnemy[NavigateMenuScript.currentLevel];
        GetComponent<EnemySpawnerCreation>().CreateEnemySpawners(levelStats[NavigateMenuScript.level].enemySpawnerNumber);
        GetComponent<BossBoxSpawner>().boss = levelBoss[NavigateMenuScript.currentLevel];
        GetComponent<ChestSpawner>().SpawnChests(levelStats[NavigateMenuScript.level].chestNumber);

        Stats enemyStats = AEnemy.standardStats;
        Stats bossStats = Boss.standardStats;
        enemyStats.Multiply(levelStats[NavigateMenuScript.level].enemyCoefficient);
        bossStats.Multiply(levelStats[NavigateMenuScript.level].enemyCoefficient);
    }

    void Start(){
        FindObjectOfType<AudioManager>().StopAll();
        FindObjectOfType<AudioManager>().Play("dentro_level");
    }

    public void gameOver()
    {
        GameObject.FindGameObjectWithTag("UI").GetComponent<MenuController>().gameOverCanvas.SetActive(true);
        FindObjectOfType<AudioManager>().StopAll();
        FindObjectOfType<AudioManager>().Play("dentro_gameover");
    }
    public void loadNextLevel()
    {
        NavigateMenuScript.level++;
        NavigateMenuScript.LoadNextLevel(false);
        NavigateMenuScript.SaveGame();
    }

    public void StartNewGame()
    {
        NavigateMenuScript.LoadLevel(true);
    }

    public void LoadFromSave(){
       
    }
}
