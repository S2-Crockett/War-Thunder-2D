using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("State References")] 
    public GameObject menuUI;
    public GameObject gameUI;
    public GameObject loseUI;

    [Header("Script References")] 
    public UIHealth healthUI;
    public UIEnemy enemyUI;
    public UINotifcation notificationUI;
    public UIScore scoreUI;
    public UIPowerCooldown cooldownUI;
    public UIPowerNotifcation powerNoticationUI;
    public UIWave waveUI;
    
    // Start is called before the first frame update
    void Start()
    {
        EnableGameHUD(false);
        EnableLoseHUD(false);
        EnableMenuHUD(true);
    }

    public void EnableMenuHUD(bool enabled)
    {
        if (enabled)
        {
            menuUI.SetActive(true);
        }
        else
        {
            menuUI.SetActive(false);
        }
    }

    public void EnableGameHUD(bool enabled)
    {
        if (enabled)
        {
            gameUI.SetActive(true);
        }
        else
        {
            gameUI.SetActive(false);
        }
    }

    public void EnableLoseHUD(bool enabled)
    {
        if (enabled)
        {
            loseUI.SetActive(true);
        }
        else
        {
            loseUI.SetActive(false);
        }
    }
}
