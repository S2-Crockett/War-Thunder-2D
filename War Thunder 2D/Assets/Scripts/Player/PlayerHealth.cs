using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Settings")] 
    public int health;

    private bool _damageTaken;

    private void Awake()
    {
        UIManager.instance.healthUI.UpdateHealth(health);
    }

    public void SetPlayerHealth(int defaultAmount)
    {
        health = defaultAmount;
        UIManager.instance.healthUI.UpdateHealth(health);
    }
    
    public void UpdateHealth(int amount)
    {
        if (!_damageTaken)
        {
            _damageTaken = true;
            StartCoroutine(ResetInvunerablePeriod(0.1f));
            
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
      
    }

    public void Die()
    {
        GameManager.instance.UpdateGameState(GameState.Lose);
        //blow up
    }

    IEnumerator ResetInvunerablePeriod(float time)
    {
        yield return new WaitForSeconds(time);
        _damageTaken = false;
    }
}
