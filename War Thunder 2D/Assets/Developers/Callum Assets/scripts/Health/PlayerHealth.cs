using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public Transform spawnpoint;
    public PlayableDirector anim;
    int lives = 1;
    public float currentHealth;

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    
    public void Respawn()
    {
        this.transform.position = spawnpoint.position;
        anim.Play();
    }

    private void Update()
    {
        
    }
    public void TakeDamage(float _damage)
    {
        if (anim.state != PlayState.Playing)
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
        if (lives == 0)
        {
            Die();
        }
    }

    public void Die()
    {
        SceneManager.LoadScene("LoseScene");
    }
}
