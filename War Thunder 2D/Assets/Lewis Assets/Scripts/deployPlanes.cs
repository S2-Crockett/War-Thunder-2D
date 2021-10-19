using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deployPlanes : MonoBehaviour
{
    public GameObject planePrefab;
    public GameObject leftSpawn;
    public GameObject rightSpawn;
    public GameObject leftDeletion;
    public GameObject rightDeletion;
    public float respawnTime = 1.0f;
    private Vector2 screenBounds;
    public Camera cam;
    
    void Start()
    {
        //cam = Camera.main;
        StartCoroutine(planeWave());
    }
    private void Update()
    {
        //screenBounds = cam.ScreenToViewportPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));

    }
    private void spawnPlane()
    {
        GameObject plane = Instantiate(planePrefab) as GameObject;
        plane.transform.position = new Vector2(leftSpawn.transform.position.x, leftSpawn.transform.position.y);
        
        GameObject plane2 = Instantiate(planePrefab) as GameObject;
        plane2.transform.position = new Vector2(rightSpawn.transform.position.x, rightSpawn.transform.position.y);
        
        /*
        if(plane.transform.position.x < leftDeletion.transform.position.x)
        {
            Destroy(plane.gameObject);
        }
        if (plane.transform.position.x > rightDeletion.transform.position.x)
        {
            Destroy(plane.gameObject);
        }
        if (plane2.transform.position.x < leftDeletion.transform.position.x)
        {
            Destroy(plane2.gameObject);
        }
        if (plane2.transform.position.x > rightDeletion.transform.position.x)
        {
            Destroy(plane2.gameObject);
        }
        */
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
