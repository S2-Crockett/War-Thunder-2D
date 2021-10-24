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
    public float currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ground")
        {
            Respawn();
            lives--;
        }
    }

    public void Respawn()
    {
        this.transform.position = spawnpoint.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);

        if (lives == 0)
        {
            Die();
        }

        if(isDead == true)
        {
            SceneManager.LoadScene("MenuScene");
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
            currentHealth = 3;
            lives--;
            Respawn();
        }
    }

    public void Die()
    {
        isDead = true;
    }
}
