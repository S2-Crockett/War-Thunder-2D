using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    [Header("Level Settings")] 
    public LevelDifficulty[] difficulties;
    
    private string currentLevelName;
    private int currentLevelIndex;

    // Start is called before the first frame update
    void Start()
    {
        SetInitialLevel();
    }

    public void SetInitialLevel()
    {
        //set the first index 
        EnemyManager.instance.setLevelDifficulty(difficulties[0]);
        EnemyManager.instance.ResetToDefaults();
        
        //set class variables
        currentLevelIndex = 0;
        currentLevelName = difficulties[0].levelName;
    }

    public void SpawnNextLevel()
    {
        currentLevelIndex++;
        EnemyManager.instance.setLevelDifficulty(difficulties[currentLevelIndex]);
        currentLevelName = difficulties[currentLevelIndex].levelName;
    }
}

[System.Serializable]
public class LevelDifficulty
{
    public string levelName;
    public int levelIndex;
    public int enemiesOnScreen;
    public int maxEnemiesToSpawn;
    public int numberOfWaves;
}
