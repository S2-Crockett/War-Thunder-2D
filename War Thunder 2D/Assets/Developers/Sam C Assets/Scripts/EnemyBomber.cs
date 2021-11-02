using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomber : MonoBehaviour
{
    
    [Header("Settings")]
    public float speed = 6.5f;
    public float enemyRange = 15f;

    [Header("Behaviour")] 
    public float viewDistance = 4.3f;
    public float shootDelay = 0.5f;

    [Header("Extras")]
    public GameObject bullets;
    
    [System.NonSerialized] 
    public EnemySpawner spawner;
    [System.NonSerialized] 
    public Camera cam;
    [System.NonSerialized] 
    public Vector2 initialVelocity;
    
    private Rigidbody2D rb;
    private Vector3 screenBounds;

    private bool setDir = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        checkDirection();
        spawningDirection();
    }
    
    
    private void spawningDirection()
    {
        if (setDir)
        {
            transform.position = new Vector3(cam.transform.position.x - 20.0f, cam.transform.position.y + 20.0f, 0.0f);
            transform.rotation = new Quaternion(90, 0, 0, 0);
            setDir = false;
        }
        rb.velocity = transform.forward;
    }
    private void checkDirection()
    {
        float camX = cam.transform.position.x;

        //Change to just redirect enemies to a different position once off the screen.
        //once player is off the screen rotate back towards the player with a random offset and carry on in 
        if (transform.position.x < camX - enemyRange)
        {
            // needs to trigger once?
            SetMovementDirection();
            RotateToTarget(1);
        }
        if (transform.position.x > camX + enemyRange)
        {
            SetMovementDirection();
            RotateToTarget(2);
        }
    }
    
    private void RotateToTarget(int direction)
    {
        if (direction == 1)
        {
            transform.rotation = new Quaternion(90, 0, 0, 0);
        }
        if (direction == 2)
        {
            transform.rotation = new Quaternion(-90, 0, 0, 0);
        }
    }

    private void SetMovementDirection()
    {
        rb.velocity = transform.forward * speed;
    }

    private void shoot()
    {
        if (shootDelay <= 0)
        {
            GameObject bullet = Instantiate(bullets);
            bullets.GetComponent<Bullet>().player = this.gameObject;
            bullets.GetComponent<Bullet>().cam = cam;
            shootDelay = 0.5f;
        }
        else
        {
            shootDelay -= Time.deltaTime;
        }
    }
}
