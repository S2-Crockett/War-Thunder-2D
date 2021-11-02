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


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        ScoreNum = 0;
        MyScoreText.text = "00000" + ScoreNum;
    }

    // Update is called once per frame
    void Update()
    {
        //scoreResult.text = "" + ScoreNum;
    }
    
}
