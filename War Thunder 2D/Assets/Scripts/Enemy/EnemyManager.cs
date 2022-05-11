using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : Singleton<EnemyManager>
{
    [Header("Enemy")] 
    public GameObject baseEnemy;

    [Header("Special Enemies")] 
    public GameObject bomberEnemy;
    
    [Header("Settings")]
    public float respawningDelay;
    public int enemySpawnOffset = 17;
    
    [NonSerialized] public LevelDifficulty currentDifficulty;
    [NonSerialized] public GameState currentGameState;
    
    private List<GameObject> m_enemyContainer = new List<GameObject>();
    
    private Vector2[] _enemySpawnPositions = new[]
    {
        new Vector2(0f, 0f), //left
        new Vector2(0f, 0f), //top
        new Vector2(0f, 0f), //right
        new Vector2(0f, 0f), //bottom
        new Vector2(0f, 0f), //bottom
        new Vector2(0f, 0f) //bottom
    };
    
    private Vector2[] _bomberSpawnPositions = new[]
    {
        new Vector2(0f, 0f), //left
        new Vector2(0f, 0f), //right
    };

    private int _enemiesSpawned;
    private int _enemiesDestroyed;
    private int _currentEnemyWave;
    private Vector3 _playerPosition;
    
    // Update is called once per frame
    void Update()
    {
        //update the position of the player
        _playerPosition = GameManager.instance.playerController.transform.position;
        float playerX = _playerPosition.x;
        float playerY = _playerPosition.y;
        
        // calculate the available spawn points for each of the planes based on
        // the player position (left, right, top, down)
        _enemySpawnPositions[0] = new Vector3(playerX - enemySpawnOffset, playerY, 0); // update left position
        _enemySpawnPositions[1] = new Vector3(playerX, playerY + enemySpawnOffset, 0); // update top position
        _enemySpawnPositions[2] = new Vector3(playerX + enemySpawnOffset, playerY, 0); // update right position
        _enemySpawnPositions[3] = new Vector3(playerX, playerY - enemySpawnOffset, 0); // update bottom position
    }
    
    public IEnumerator StartSpawningEnemy()
    {
        // if we are currently in the playing game state
        while (currentGameState == GameState.Playing)
        {
            // spawn initial wave of enemies - decrease enemies spawned to spawn more.
            // check if we currently have less enemies spawned than currently on the screen
            // and also if the amount we have already destroyed and spawned are less than
            // the max we allow on screen
            if (_enemiesSpawned < currentDifficulty.enemiesOnScreen &&
                _enemiesDestroyed + _enemiesSpawned < currentDifficulty.maxEnemiesToSpawn)
            {
                // something here or near enough here is causing an error?
                SpawnBaseEnemy();
                yield return new WaitForSeconds(respawningDelay);
                _enemiesSpawned++;
            }
            
            yield return new WaitForSeconds(1);
        }
    }

    public void ResetToDefaults()
    {
        _enemiesSpawned = 0;
        _enemiesDestroyed = 0;
        _currentEnemyWave = 0;

        for (int i = 0; i < m_enemyContainer.Count; i++)
        {
            if (m_enemyContainer[i] != null)
            {
                Destroy(m_enemyContainer[i]);
            }
        }

        m_enemyContainer = new List<GameObject>();
    }

    public void StartSpawningSpecialEnemies()
    {
        
    }

    private void SpawnBaseEnemy()
    {
        // spawn a plane and set all the default components in the enemy class
        GameObject plane = Instantiate(baseEnemy) as GameObject;
        m_enemyContainer.Add(plane);
        
        // calculate a random position for the enemy to spawn
        int index = Random.Range(0, _enemySpawnPositions.Length);
        plane.transform.position = _enemySpawnPositions[index];
        
        // if the plane is positioned to low then only enable spawning on (top,left,right)
        if (_playerPosition.y < 35)
        {
            if (Random.value < 0.5f)
            {
                index = 0; // left
            }
            else
            {
                index = 2; // right 
            }
            plane.transform.position = _enemySpawnPositions[index];
        }
        else
        {
            index = Random.Range(0, 3);
            plane.transform.position = _enemySpawnPositions[index];
        }

        if (index == 0)
        {
            //left
            plane.GetComponent<Enemy>().initialVelocity = new Vector2(1,0);
        }
        else if (index == 1)
        {
            //top
            plane.GetComponent<Enemy>().initialVelocity = new Vector2(0,-1);
        }
        else if (index == 2)
        {
            //right
            plane.GetComponent<Enemy>().initialVelocity = new Vector2(-1,0);
        }
        else if (index == 3)
        {
            //bottom
            plane.GetComponent<Enemy>().initialVelocity = new Vector2(0, 1);
        }
    }

    public void EnemyDestroyed()
    {
        _enemiesDestroyed++;
        UIManager.instance.enemyUI.DecreaseEnemyAmount(1);
        GameManager.instance.UpdateScore(100);
        GameManager.instance.UpdateEnemiesDestroyed(1);

        if (_enemiesDestroyed <= currentDifficulty.maxEnemiesToSpawn)
        {
            _enemiesSpawned--;
        }
          
        if(_enemiesDestroyed == currentDifficulty.maxEnemiesToSpawn)
        {
            if (_currentEnemyWave != currentDifficulty.numberOfWaves)
            {
                _enemiesDestroyed = 0;
                _currentEnemyWave++;
                UIManager.instance.waveUI.UpdateLevel(_currentEnemyWave, currentDifficulty.levelIndex);
                UIManager.instance.notificationUI.SetNotification("NEW WAVE SPAWNING", 2.0f);
            }
            else
            {
                
                StartCoroutine(resetWaves());
            }
        }
    }
    
    public void setLevelDifficulty(LevelDifficulty difficulty)
    {
        currentDifficulty = difficulty;
        // when a new level begins, calculate the max number of enemies that can spawn
        // then update the ui 
        int totalEnemies = currentDifficulty.maxEnemiesToSpawn * (currentDifficulty.numberOfWaves + 1);
        UIManager.instance.enemyUI.SetEnemyAmount(totalEnemies);
        UIManager.instance.waveUI.UpdateLevel(_currentEnemyWave, currentDifficulty.levelIndex);
    }
    private IEnumerator resetWaves()
    {
        UIManager.instance.notificationUI.SetNotification("NEW ROUND", 2.0f);
        
        yield return new WaitForSeconds(5.0f);
        
        LevelManager.instance.SpawnNextLevel();
        UIManager.instance.waveUI.UpdateLevel(_currentEnemyWave, currentDifficulty.levelIndex);
        _enemiesSpawned = 0;
        _enemiesDestroyed = 0;
        _currentEnemyWave = 0;
    }
}