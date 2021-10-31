using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPowerup : MonoBehaviour
{
    [System.NonSerialized] 
    public ScoreScript score;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (score.ScoreNum < 1000)
            {
                score.ScoreNum += 500;
                score.MyScoreText.text = "000" + score.ScoreNum;
            }
            else if (score.ScoreNum >= 1000)
            {
                score.ScoreNum += 500;
                score.MyScoreText.text = "00" + score.ScoreNum;
            }
            Destroy(gameObject);
        }
    }
}
