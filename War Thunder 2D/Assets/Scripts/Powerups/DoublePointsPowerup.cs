using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePointsPowerup: MonoBehaviour
{
    [Header("Powerup Settings")] 
    public float powerupTime;
    
    [Header("Audio")]
    public AudioClip powerupClip;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SoundManager.instance.PlayEffectOneShot(powerupClip);
            UIManager.instance.scoreUI.ActiveDoubleScore(powerupTime);
            UIManager.instance.cooldownUI.StartDoublePointsCooldown(powerupTime);
            UIManager.instance.powerNoticationUI.SetNotification("DOUBLE POINTS ACTIVATED", 2.0f);
            Destroy(gameObject);
        }
    }
    
    private void Update()
    {
        transform.Translate(((transform.up * -1) * 3 * Time.deltaTime));
    }

}
