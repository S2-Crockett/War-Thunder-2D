using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    public int LevelNum;
    public Text LevelText;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Level 1");
        LevelNum = 1;
    }

    // Update is called once per frame
    void Update()
    {
        LevelText.text = "LEVEL: " + LevelNum;
    }
}
