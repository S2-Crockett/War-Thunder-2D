using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject startButton, quitButton;
    // Start is called before the first frame update
    void Start()
    {
        
 
    }

    // Update is called once per frame
    // This is a really simple way of checking what has been submitted based on the selected button,
    // would probably be more beneficial to move this to an event based system.
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            if (EventSystem.current.currentSelectedGameObject == startButton)
            {
                Debug.Log("load next scene");
            }
            else if(EventSystem.current.currentSelectedGameObject == quitButton)
            {
                Debug.Log("quit the game");
            }
        }
    }
    
    
}
