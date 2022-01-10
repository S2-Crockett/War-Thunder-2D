using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWave : MonoBehaviour
{
    [Header("References")] 
    public Text levelText;
    // Start is called before the first frame update
    
    public void UpdateLevel(int wave, int level)
    {
        levelText.text = wave.ToString() + " - " + level.ToString();
    }
}
