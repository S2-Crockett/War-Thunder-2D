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

    private PlayerMovement _playerMovement;
    
    private void Start()
    {
        UpdateGameState(GameState.Menu);
        _playerMovement = playerController.GetComponent<PlayerMovement>();
        _playerMovement.currentGameState = state;
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
                break;
        }
    }

    public void UpdateScore(int amount)
    {
        UIManager.instance.scoreUI.UpdateScore(amount);
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
        //allow input to select name
        //input to select game type?
    }
    
    private void HandlePlayingState()
    {
        _playerMovement.currentGameState = GameState.Playing;
        UIManager.instance.EnableMenuHUD(false);
        UIManager.instance.EnableGameHUD(true);
        
        EnemyManager.instance.currentGameState = GameState.Playing;
        StartCoroutine(EnemyManager.instance.StartSpawningEnemy());
        //update score.. 
    }

    private void HandleLoseState()
    {
        
    }
}

//States that are present throughout gameplay.
public enum GameState
{
    Menu,
    Playing,
    Lose
}
