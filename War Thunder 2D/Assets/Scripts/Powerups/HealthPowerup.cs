using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthPowerup : MonoBehaviour
{
    
    [Header("Audio")]
    public AudioClip powerupClip;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EventManager.instance.UpdatePlayerHealth(1);
            Destroy(gameObject);
        }
    }
    
    private void Update()
    {
        transform.Translate(((transform.up * -1) * 3 * Time.deltaTime));
    }
}
