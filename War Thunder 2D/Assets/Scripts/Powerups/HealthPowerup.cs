using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthPowerup : MonoBehaviour
{
    [System.NonSerialized]
    public PlayerHealth health;
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            health.currentHealth = health.currentHealth + 1.1f;
            Destroy(gameObject);
        }
    }
}
