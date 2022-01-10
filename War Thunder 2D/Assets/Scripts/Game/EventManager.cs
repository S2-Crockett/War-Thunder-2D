using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    [Header("Player")] 
    public PlayerHealth playerHealth;

    public void UpdatePlayerHealth(int amount)
    {
        playerHealth.UpdateHealth(amount);
    }
    
}
