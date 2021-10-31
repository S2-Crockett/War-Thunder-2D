using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public Transform spawnpoint;
    bool isDead = false;
    int lives = 3;
    public float currentHealth;

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    
    public void Respawn()
    {
        this.transform.position = spawnpoint.position;
    }

    private void Update()
    {
        if (lives == 0)
        {
            Die();
        }
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            //player hurt
        }
        else
        {
            currentHealth = 3.3f;
            lives--;
            Respawn();
        }
    }

    public void Die()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
