using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Settings")] 
    public int health = 3;

    [Header("References")] 
    public GameObject destroyPrefab;

    private bool _dead = false;
    
    public void UpdateHealth(int amount)
    {
        health += amount;
        if (health <= 0)
        {
            if(!_dead)
            Die();
        }
    }

    public void Die()
    {
        _dead = true;
        
        EnemyManager.instance.EnemyDestroyed();
        GameObject boom = Instantiate(destroyPrefab);
        boom.transform.position = transform.position;
        Destroy(gameObject);
    }
}
