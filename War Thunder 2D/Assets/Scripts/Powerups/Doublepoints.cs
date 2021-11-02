using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doublepoints : MonoBehaviour
{
    
    public ScoreScript score;

    private bool doublePoints_active = false;
    public float power_up_duration = 10.0f;
    public float time = 0.0f;


    private void Start()
    {

        doublePoints_active = false;
    

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            doublePoints_active = true;
        }
        else if(time >= power_up_duration)
        {
            doublePoints_active = false;
        }
    }

    private void Update()
    {
        if(doublePoints_active)
        {
            score.ScoreNum *= 2;

            time += Time.deltaTime;
        }
        else if(doublePoints_active == false)
        {
            score.ScoreNum *= 1;
        }
    }
}
