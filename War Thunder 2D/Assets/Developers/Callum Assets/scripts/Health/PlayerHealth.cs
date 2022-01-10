using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Settings")] 
    public int health;

    private void Awake()
    {
        UIManager.instance.healthUI.UpdateHealth(health);
    }
    
    public void UpdateHealth(int amount)
    {
        health += amount;
        if (health <= 0)
        {
            UIManager.instance.healthUI.UpdateHealth(health);
            Die();
        }
        else
        {
            UIManager.instance.healthUI.UpdateHealth(health);
        }
    }

    public void Die()
    {
        GameManager.instance.UpdateGameState(GameState.Lose);
    }


}
