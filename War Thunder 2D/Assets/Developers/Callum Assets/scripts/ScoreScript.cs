using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text MyScoreText;
    public int ScoreNum;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        ScoreNum = 0;
        MyScoreText.text = "00000" + ScoreNum;
    }

    // Update is called once per frame
    void Update()
    {
            
    }
    
}
