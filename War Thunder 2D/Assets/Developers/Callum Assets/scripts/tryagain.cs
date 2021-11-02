using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class tryagain : MonoBehaviour
{
    public GameObject canvas;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(canvas);
            SceneManager.LoadScene("MenuScene");
        }
    }
}
