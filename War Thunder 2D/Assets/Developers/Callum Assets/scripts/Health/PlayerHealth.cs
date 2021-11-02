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


    private void Awake()
    {
        currentHealth = startingHealth;
    }


    public void TakeDamage(float _damage)
    {
        if (anim.state != PlayState.Playing)
        {
            currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
            if (currentHealth > 1)
            {
                //player hurt
            }
            else
            {
                Die();
            }
        }
    }

    public void Die()
    {
        SceneManager.LoadScene("LoseScene");
    }


}
