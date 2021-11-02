using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPowerup : MonoBehaviour
{
    [System.NonSerialized] 
    public  ScoreScript score;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            score.AddScore(500);
            Destroy(gameObject);
        }
    }
}
