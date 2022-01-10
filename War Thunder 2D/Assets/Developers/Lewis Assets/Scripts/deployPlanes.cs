using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deployPlanes : MonoBehaviour
{
    public GameObject planePrefab;
    public GameObject aircraftCarrier;
    public float cutsceneTime = 3.0f;
    private bool cutsceneFinished = false;
    public float respawnTime = 1.0f;
    private Vector3 screenBounds;
    private Camera cam;

    public int numPlanesSpawning;
    public int numPlanesMax;
    
    public GameObject player;

    private Vector3 leftSpawnPos = new Vector3(0,0,0);
    private Vector3 rightSpawnPos = new Vector3(0,0,0);
    
    void Start()
    {
        cam = Camera.main;
        StartCoroutine(planeWave());
    }
    private void Update()
    {
        // screen width is hardcoded to 8, need to change this if we ever change the screen size.
        leftSpawnPos = new Vector3(cam.transform.position.x - 8 , cam.transform.position.y,0);
        rightSpawnPos = new Vector3(cam.transform.position.x + 8 , cam.transform.position.y, 0);
    }
    private void spawnPlane()
    {
        GameObject plane = Instantiate(planePrefab) as GameObject;
        plane.transform.position = leftSpawnPos;
        
        GameObject plane2 = Instantiate(planePrefab) as GameObject;
        plane2.transform.position = rightSpawnPos;

    }
    IEnumerator planeWave()
    {
        while (true) // Can be changed once main menu is created so we know when to start the game.
        {
            if(cutsceneFinished == false)
            {
                yield return new WaitForSeconds(cutsceneTime);
                cutsceneFinished = true;
            }
            else
            {
                Destroy(aircraftCarrier);
                spawnPlane();
                yield return new WaitForSeconds(respawnTime);
            }
            
        }
    }
    
}
