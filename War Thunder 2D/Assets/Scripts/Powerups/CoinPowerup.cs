using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPowerup : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip powerupClip;

    private void Start()
    {
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.instance.UpdateScore(1000);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Translate(((transform.up * -1) * 3 * Time.deltaTime));
    }
}
