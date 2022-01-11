using System;
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
    public float currentHealth;
    private SpriteRenderer spriteRenderer;
    public Sprite[] Explosions;
    public float timer = 1f;

    public bool die = false;

    public enum Dead
    {
        ONE,
        TWO,
        THREE,
        FOUR,
        DEAD
    }

    public Dead dead;
    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        currentHealth = startingHealth;
        dead = Dead.ONE;
    }

    private void Update()
    {
        if (die)
        {
            GetComponent<SpriteChange>().enabled = false;
            Die();
        }
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
                die = true;
            }
        }
    }

    public void Die()
    {
        switch (dead)
        {
            case Dead.ONE:
            {
                transform.localScale = new Vector3(2f, 2f, 2f);
                spriteRenderer.sprite = Explosions[0];
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = 0.5f;
                    dead = Dead.TWO;
                }
                break;
            }
            case Dead.TWO:
            {
                spriteRenderer.sprite = Explosions[1];
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = 0.5f;
                    dead = Dead.THREE;
                }
                break;
            }
            case Dead.THREE:
            {
                spriteRenderer.sprite = Explosions[2];
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = 0.5f;
                    dead = Dead.FOUR;
                }
                break;
            }
            case Dead.FOUR:
            {
                spriteRenderer.sprite = Explosions[3];
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = 0.5f;
                    dead = Dead.DEAD;
                }
                break;
            }
            case Dead.DEAD:
            {
                SceneManager.LoadScene("LoseScene");
                break;
            }
        }
    }
}
