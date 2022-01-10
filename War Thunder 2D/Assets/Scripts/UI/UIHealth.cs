using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    [Header("References")] 
    public Text healthText;

    private int healthAmount = 0;
    
    public void UpdateHealth(int amount)
    {
        healthAmount += amount;
        healthText.text = healthAmount.ToString();
    }
}
