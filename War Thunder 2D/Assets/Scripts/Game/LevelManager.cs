using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Level Settings")] 
    public LevelDifficulty[] difficulties;

    [Header("Prefabs")] 
    public EnemySpawner spawner;
    public Text levelText;
    
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
        spawner.setLevelDifficulty(difficulties[0]);
        
        //set class variables
        currentLevelIndex = difficulties[0].levelIndex;
        currentLevelName = difficulties[0].levelName;
        
        //update our level text
        updateLevelText(currentLevelName);

    }

    public void SpawnNextLevel()
    {
        currentLevelIndex++;
        spawner.setLevelDifficulty(difficulties[currentLevelIndex]);
        currentLevelName = difficulties[currentLevelIndex].levelName;
        
        //update our level text
        updateLevelText(currentLevelName);
    }

    private void updateLevelText(string levelName)
    {
        levelText.text = levelName;
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
