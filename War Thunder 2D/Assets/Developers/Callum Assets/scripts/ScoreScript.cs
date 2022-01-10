using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text MyScoreText;
    public int ScoreNum;
    public GameObject player;
    public Text scoreResult;

    private bool bShouldDouble = false;
    public float power_up_duration = 10.0f;
    float time = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        ScoreNum = 0;
        MyScoreText.text = "00000" + ScoreNum;
    }

    public void AddScore(int amountToAdd)
    {
        if (bShouldDouble)
        {
            CalculateScore(amountToAdd *= 2);
        }
        else
        {
            CalculateScore(amountToAdd);
        }
    }

    private void CalculateScore(int amount)
    {
        if (ScoreNum > 1000)
        {
            ScoreNum = ScoreNum + amount;
            MyScoreText.text = "000" + ScoreNum;
        }
        else if (ScoreNum <= 1000)
        {
            ScoreNum = ScoreNum + amount;
            MyScoreText.text = "00" + ScoreNum;
        }
    }

    public void DoubleScore()
    {
        bShouldDouble = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (bShouldDouble)
        {
            time += Time.deltaTime;

            if(time >= power_up_duration)
            {
                time = 0.0f;
                bShouldDouble = false;
            }
        }
    }
}
