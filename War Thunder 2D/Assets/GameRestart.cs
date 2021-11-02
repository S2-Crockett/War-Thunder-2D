using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameRestart : MonoBehaviour
{
    public GameObject restartButton, quitButton;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    // This is a really simple way of checking what has been submitted based on the selected button,
    // would probably be more beneficial to move this to an event based system.
    void Update()
    {
        if (Input.GetAxis("Submit") != 0)
        {
            if (EventSystem.current.currentSelectedGameObject == restartButton)
            {
                SceneManager.LoadScene("EnemyTestScene");
                Destroy(canvas);
            }
            else if (EventSystem.current.currentSelectedGameObject == quitButton)
            {
                Application.Quit();
            }
        }
    }


}
