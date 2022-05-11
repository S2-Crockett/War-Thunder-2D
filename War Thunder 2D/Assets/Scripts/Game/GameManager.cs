using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState state;
    
    [Header("References")] 
    public GameObject playerController;

    [Header("PlayerStats")] 
    public int playerScore;
    public int enemysDestroyed;

    private PlayerController _playerMovement;
    
    private void Start()
    {
        UpdateGameState(GameState.Menu);
        _playerMovement = playerController.GetComponent<PlayerController>();
        _playerMovement._currentGameState = state;
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.Menu:
                if (Input.GetButtonDown("Submit"))
                {
                    UpdateGameState(GameState.Playing);
                }
                break;
            case GameState.Playing:
                break;
            case GameState.Lose:
                if (Input.GetButtonDown("Submit"))
                {
                    UpdateGameState(GameState.Menu);
                }
                break;
        }
    }

    public void UpdateScore(int amount)
    {
        playerScore += amount;
        UIManager.instance.scoreUI.UpdateScore(amount);
    }

    public void SetScore(int amount)
    {
        playerScore = amount;
        UIManager.instance.scoreUI.SetScore(playerScore);
    }

    public void UpdateEnemiesDestroyed(int amount)
    {
        enemysDestroyed += amount;
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;
        switch (newState)
        {
            case GameState.Menu:
                HandleMenuState();
                break;
            case GameState.Playing:
                HandlePlayingState();
                break;
            case GameState.Lose:
                HandleLoseState();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    private void HandleMenuState()
    {
        UIManager.instance.EnableLoseHUD(false);
        UIManager.instance.EnableMenuHUD(true);
        
        // set the intial player position
        playerController.transform.position = new Vector3(0,20.0f,0);
        playerController.transform.rotation = Quaternion.Euler(0, 0, -90.0f);
        
        
        
        // set player health - reset everything back to default??
        // reset waves in enemy manager and level manager level back to 0
        // reset all of the ui back to its default variables
    }
    
    private void HandlePlayingState()
    {
        _playerMovement._currentGameState = GameState.Playing;
        UIManager.instance.EnableMenuHUD(false);
        UIManager.instance.EnableGameHUD(true);
        
        EnemyManager.instance.currentGameState = GameState.Playing;
        EventManager.instance.playerHealth.SetPlayerHealth(5);
        LevelManager.instance.SetInitialLevel();
        SetScore(0);
        
        StartCoroutine(EnemyManager.instance.StartSpawningEnemy());
    }

    private void HandleLoseState()
    {
        _playerMovement._currentGameState = GameState.Lose;
        EnemyManager.instance.currentGameState = GameState.Lose;
        
        UIManager.instance.EnableGameHUD(false);
        
        //lose hud needs an animation 2 seconds, just to witness player explosion
        
        UIManager.instance.EnableLoseHUD(true);
        //display the hud details (player score & enemies destroyed)
    }
}

//States that are present throughout gameplay.
public enum GameState
{
    Menu,
    Playing,
    Lose
}
