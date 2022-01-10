using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthPowerup : MonoBehaviour
{
    [System.NonSerialized]
    public PlayerHealth health;
    
    public AudioClip powerupClip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            audioSource.Play();
            //health.currentHealth = health.currentHealth + 1.1f;
            Destroy(gameObject);
        }
    }
}
