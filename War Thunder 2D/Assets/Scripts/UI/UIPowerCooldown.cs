using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPowerCooldown : MonoBehaviour
{
    [Header("References")] 
    public Text cooldownText;
    
    private float _powerupTime;
    private bool _active;

    public void StartDoublePointsCooldown(float time)
    {
        gameObject.SetActive(true);
        _active = true;
        StartCoroutine(StartCountdown(time));
    }

    IEnumerator StartCountdown(float time)
    {
        _powerupTime = time;
        while (_powerupTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            _powerupTime--;
            cooldownText.text = _powerupTime.ToString("#");
        }
        CountdownComplete();
    }
    
    private void CountdownComplete()
    {
        gameObject.SetActive(false);
    }
}
