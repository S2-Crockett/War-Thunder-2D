using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseSceneScore : MonoBehaviour
{
    public Text finalScoreText;
    private GameObject finalScore;
    public int score;
    
    // Start is called before the first frame update
    void Start()
    {
        
        finalScore = GameObject.Find("Player");
        score = finalScore.GetComponent<ScoreScript>().ScoreNum;
        Destroy(finalScore);
        finalScoreText.text = "" + score;
        
        
    }
    void Update()
    {
        finalScoreText.text = "" + score;
    }
}
