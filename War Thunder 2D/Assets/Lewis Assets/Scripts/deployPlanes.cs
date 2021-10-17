using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deployPlanes : MonoBehaviour
{
    public GameObject planePrefab;
    public float respawnTime = 1.0f;
    private Vector2 screenBounds;
    public Camera cam;
    
    void Start()
    {
        cam = Camera.main;
        screenBounds = cam.ScreenToViewportPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
        StartCoroutine(planeWave());
    }
    private void spawnPlane()
    {
        GameObject plane = Instantiate(planePrefab) as GameObject;
        plane.transform.position = new Vector2(screenBounds.x * 10, Random.Range(-screenBounds.y, screenBounds.y));
        
        GameObject plane2 = Instantiate(planePrefab) as GameObject;
        plane2.transform.position = new Vector2(-screenBounds.x * 10, Random.Range(-screenBounds.y, screenBounds.y));
    }
    IEnumerator planeWave()
    {
        while (true) // Can be changed once main menu is created so we know when to start the game.
        {
            yield return new WaitForSeconds(respawnTime);
            spawnPlane();
        }
    }
    
}
