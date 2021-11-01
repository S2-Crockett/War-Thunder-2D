using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public Transform spawnpoint;
    public PlayableDirector animation;
    int lives = 3;
    public float currentHealth;

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    
    public void Respawn()
    {
        this.transform.position = spawnpoint.position;
        animation.Play();
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
        if (animation.state != PlayState.Playing)
        {
            currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
            if (currentHealth > 0)
            {
                //player hurt
            }
            else
            {
                currentHealth = 3f;
                lives--;
                Respawn();
            }
        }
    }

    public void Die()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
