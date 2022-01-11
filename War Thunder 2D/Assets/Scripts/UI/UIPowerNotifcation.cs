using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPowerNotifcation : MonoBehaviour
{
    [Header("References")] 
    public Text notificationText;

    public void SetNotification(string text, float displayTime)
    {
        gameObject.SetActive(true);
        notificationText.text = text;
        StartCoroutine(DisplayNotification(displayTime));
    }
    
    IEnumerator DisplayNotification(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
        notificationText.text = null;
    }
}
