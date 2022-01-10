using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemy : MonoBehaviour
{
    [Header("References")] 
    public Text enemyAmountText;

    private int _enemyAmount = 0;
    
    //updates the enemy ui with the amount of enemies left to destroy for this current level
    public void UpdateEnemyAmount(int amount)
    {
        _enemyAmount += amount;
        enemyAmountText.text = _enemyAmount.ToString();
    }
}
