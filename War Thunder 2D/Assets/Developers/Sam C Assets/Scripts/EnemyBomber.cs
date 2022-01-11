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
    public AudioClip shootingAudio;
    public AudioClip destroyAudio;
    private AudioSource audioSource;

    [System.NonSerialized] 
    public EnemySpawner spawner;
    [System.NonSerialized] 
    public Camera cam;
    [System.NonSerialized] 
    public Vector2 initialVelocity;
    
    private Rigidbody2D rb;
    private Vector3 screenBounds;
    public GameObject destructionPrefab;
    
    private int direction_;
    private float bomberTimer = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        spawningDirection();
        BomberFire();

    }
    
    
    private void spawningDirection()
    {
        if (rb.velocity.x == 0 || rb.velocity.y == 0)
        {
            // x will always be 0 to head towards the player, y will have a random offset
            if (initialVelocity.x == 1)
            {
                rb.velocity = new Vector2(initialVelocity.x, 0) * speed;
                rb.rotation = -90;
                direction_ = 1;
            }
            else
            {
                rb.velocity = new Vector2(initialVelocity.x, 0) * speed;
                rb.rotation = 90;
                direction_ = 2;
            }
        }
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
        if (transform.position.y < cam.transform.position.y + 10 && 
            transform.position.y > cam.transform.position.y - 2)
        {
            audioSource.PlayOneShot(shootingAudio, 0.5f);
            if (direction_ == 1)
            {
                GameObject bullet = Instantiate(bullets);
                bullets.GetComponent<Bullet>().offset = 90;
            }

            if (direction_ == 2)
            {
                GameObject bullet = Instantiate(bullets);
                bullets.GetComponent<Bullet>().offset = -90;
            }

            bullets.GetComponent<Bullet>().player = this.gameObject;
            bullets.GetComponent<Bullet>().cam = cam;
        }
    }

    private void BomberFire()
    {
        bomberTimer -= 1.0f * Time.deltaTime;
        if (bomberTimer <= 0)
        {
            shoot();
            bomberTimer = 1.0f;
        }
    }

    public void MyOwnDestroy()
    {
        audioSource.PlayOneShot(destroyAudio, 0.5f);
        GameObject boom = Instantiate(destructionPrefab);
        spawner.AddBomberScore();
        boom.transform.position = transform.position;
        Destroy(gameObject);
    }
}
