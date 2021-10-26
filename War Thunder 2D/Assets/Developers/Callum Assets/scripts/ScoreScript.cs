using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text MyScoreText;
    public int ScoreNum;

    // Start is called before the first frame update
    void Start()
    {
        ScoreNum = 0;
        MyScoreText.text = "Score : " + ScoreNum;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.R))
        {
            ScoreNum++;
            MyScoreText.text = "Score : " + ScoreNum;
        }       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            ScoreNum++;
            Debug.Log("We got a Coin");
            Destroy(collision.gameObject);
        }
    }

}
