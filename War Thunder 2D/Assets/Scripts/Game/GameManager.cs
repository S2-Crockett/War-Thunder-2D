using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState state;
    private void Start()
    {
        UpdateGameState(GameState.Intro);
    }
    
    public void UpdateGameState(GameState newState)
    {
        state = newState;
        switch (newState)
        {
            case GameState.Intro:
                HandleIntroState();
                break;
            case GameState.Menu:
                HandleIntroState();
                break;
            case GameState.BeginPlaying:
                HandleBeginPlayState();
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

    private void HandleIntroState()
    {
        //enable intro ui
        //display intro part of the ui
        
        // plane movement starts along x axis.. (flying side ways)
        // set menu state.
    }

    private void HandleMenuState()
    {
        //enable menu ui
        //display menu ui
        
        //allow input to select name
    }

    private void HandleBeginPlayState()
    {
        //disable menu ui
        //start countdown timer.
        //when count down is finished move to play state 
    }

    private void HandlePlayingState()
    {
        //enable in game ui
        //display in game ui
        
        //allow input 
        //start spawning enemies
    }

    private void HandleLoseState()
    {
        
    }
}

//States that are present throughout gameplay.
public enum GameState
{
    Intro,
    Menu,
    BeginPlaying,
    Playing,
    Lose
}
