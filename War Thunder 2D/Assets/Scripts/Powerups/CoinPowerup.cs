using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPowerup : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip powerupClip;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundManager.instance.PlayEffectOneShot(powerupClip);
            GameManager.instance.UpdateScore(1000);
            UIManager.instance.powerNoticationUI.SetNotification("1000 SCORE COLLECTED", 2.0f);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Translate(((transform.up * -1) * 3 * Time.deltaTime));
    }
}
