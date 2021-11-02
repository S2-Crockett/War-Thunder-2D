using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doublepoints : MonoBehaviour
{
    
    public ScoreScript score;


    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            score.DoubleScore();
            Destroy(gameObject);
        }
    }

}
