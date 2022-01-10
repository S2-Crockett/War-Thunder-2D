using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    [Header("References")] 
    public Text scoreText;

    private int _scoreAmount;
    private bool _doublePoints;
    
    // Start is called before the first frame update
    void Start()
    {
        _scoreAmount = 0;
        scoreText.text = "00000" + _scoreAmount; 
    }

    public void UpdateScore(int amount)
    {
        if (_doublePoints)
        {
            CalculateScore(amount *= 2);
        }
        else
        {
            CalculateScore(amount);
        }
    }

    private void CalculateScore(int amount)
    {
        if (_scoreAmount > 1000)
        {
            _scoreAmount = _scoreAmount + amount;
            scoreText.text = "000" + _scoreAmount;
        }
        else if (_scoreAmount <= 1000)
        {
            _scoreAmount = _scoreAmount + amount;
            scoreText.text = "00" + _scoreAmount;
        }
    }

    public void ActiveDoubleScore(float time)
    {
        _doublePoints = true;
        StartCoroutine(DoubleScoreCooldown(time));
    }

    private IEnumerator DoubleScoreCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        _doublePoints = false;
    }
}
