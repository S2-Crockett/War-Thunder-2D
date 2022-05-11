using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    [Header("Settings")] 
    public float timeToDisplay = 2.0f;

    [Header("References")] 
    public GameObject panel;
    public Text scoreText;
    public Text enemiesText;
    public UITextFlash flash;

    private void OnEnable()
    {
        StartCoroutine(DelayEnable(timeToDisplay));
        scoreText.text = GameManager.instance.playerScore.ToString();
        enemiesText.text = GameManager.instance.enemysDestroyed.ToString();
    }

    private void OnDisable()
    {
        panel.SetActive(false);
    }

    private IEnumerator DelayEnable(float time)
    {
        yield return new WaitForSeconds(time);
        panel.SetActive(true);
        flash.StartFlash();
    }
}
