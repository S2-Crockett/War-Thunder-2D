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
            UIManager.instance.scoreUI.ActiveDoubleScore(powerupTime);
            Destroy(gameObject);
        }
    }

}
