using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy_Spawner : MonoBehaviour {

    // Cooldown settings.
    [SerializeField] private float waveCooldown;
    [SerializeField] private float enemySpawnCooldown;
    [SerializeField] private float bossSpawnCooldown;

    // Enemy Count Settings.
    [SerializeField] private int enemyCount;
    [HideInInspector] public int totalEnemyCount;
    [SerializeField] private int maxEnemiesAllowed;

    // Enemy GameObjects to use.
    [SerializeField] private GameObject[] enemyArray;
    [SerializeField] private GameObject[] bossArray;
    private GameObject enemy;
    private GameObject boss;
    [SerializeField] private GameObject[] spawnNode_GameObjects;
    [SerializeField] private GameObject[] bossSpawnNode_GameObjects;
    private bool IsThisTheIntroSCreeN;
    [Space(10)]
    [SerializeField] private GameObject uiCanvass_OBJ;
    [SerializeField] private UI_Controller uiController_SCRIPT;
    [SerializeField] private Text wavedisplay_TEXT;

    // Spawn vars needed.
    private float spawnY = 0.0f;
    [HideInInspector] public bool isBossAlive = false;
    private bool isGameOver = false;

    // References to the wave display.
    private Text waveDisplayText;
    [SerializeField] private GameObject waveDisplayOBJ;

    // The _currentwave allows us to set a private value here so external sources cannot alter this since we also set a get function under the currentWave int so that it always references the private int.
    private int _currentWave = 1;
    [HideInInspector] public int currentWave { get { return _currentWave; } } 
    


    private void Awake()
    {
        // Get UI bool to see if this is the start screen or not.
        if (SceneManager.GetActiveScene().buildIndex == 0) { IsThisTheIntroSCreeN = true; } else { IsThisTheIntroSCreeN = false; }

        // Start the spawn machine.
        StartCoroutine(SpawnWaves());
    }


    IEnumerator SpawnWaves()
    {
        if (isGameOver == false && totalEnemyCount <= maxEnemiesAllowed)
        {
            while (true)
            {
                if (IsThisTheIntroSCreeN)
                {
                    DisplayWaveCount();
                    yield return new WaitForSeconds(waveCooldown);
                    RemoveWaveCount();
                }

                for (int i = 0; i < enemyCount; i++)
                {
                    if (totalEnemyCount < maxEnemiesAllowed)
                    {
                        var rand = Random.Range(0, spawnNode_GameObjects.Length - 1);
                        Vector3 spawnPosition = new Vector3(spawnNode_GameObjects[rand].transform.position.x, spawnY, spawnNode_GameObjects[rand].transform.position.z);
                        SpawnEnemy(i, spawnPosition);
                        yield return new WaitForSeconds(Random.Range(enemySpawnCooldown - 1, enemySpawnCooldown + 1));
                        totalEnemyCount++;
                    }
                }

                yield return new WaitForSeconds(Random.Range(bossSpawnCooldown - 1, bossSpawnCooldown + 1));

                var randBoss = Random.Range(0, bossSpawnNode_GameObjects.Length - 1);
                Vector3 bossSpawnPosition = new Vector3(spawnNode_GameObjects[randBoss].transform.position.x, spawnY, spawnNode_GameObjects[randBoss].transform.position.z);
                totalEnemyCount++;

                SpawnBoss(bossSpawnPosition);
                while (isBossAlive) { yield return null; }

                _currentWave = ++_currentWave; 
            }
        }
    }
    private void DisplayWaveCount()
    {
        waveDisplayOBJ.gameObject.SetActive(true);
        wavedisplay_TEXT.text = "Wave " + currentWave.ToString();
    }
    private void RemoveWaveCount()
    {
        waveDisplayOBJ.gameObject.SetActive(false);
    }
    private void SpawnEnemy(int i, Vector3 spawnPosition)
    {
        enemy = enemyArray[UnityEngine.Random.Range(0, enemyArray.Length - 1)];
        Quaternion spawnRotation = Quaternion.identity;
        var temp = Instantiate(enemy, spawnPosition, spawnRotation);
        InformNewSpawn("Enemy", i, temp);
    }
    private void SpawnBoss(Vector3 spawnPosition)
    {
        isBossAlive = true;
        boss = bossArray[UnityEngine.Random.Range(0, bossArray.Length - 1)];
        Quaternion spawnRotation = Quaternion.identity;
        GameObject temp = Instantiate(boss, spawnPosition, spawnRotation);
        InformNewSpawn("Boss", 0, temp);
    }
    private void InformNewSpawn(string title, int spawnNumber, GameObject spawnObject)
    {
        // Adding info to the enemy.
        spawnObject.name = title + currentWave + "_s" + spawnNumber;
    }
}
