using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public GameObject bullet;
    public GameObject player;
    public Camera cam;
    
    public AudioClip shootingAudio;
    private AudioSource audioSource;

    private bool dead = false;
    public bool powerUpActive = false;
    private float powerUpTimer = 3.0f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        dead = GetComponent<PlayerHealth>().die;
        if (!dead)
        {
            if (Input.GetButtonDown("Submit"))
            {
                PlayerShoot();
                audioSource.PlayOneShot(shootingAudio, 0.5f);
            }
        }

        if (powerUpActive)
        {
            powerUpTimer -= Time.deltaTime;
            if (powerUpTimer <= 0)
            {
                powerUpActive = false;
            }
        }
    }

    private void PlayerShoot()
    {
        GameObject bullets = Instantiate(bullet);
        bullets.GetComponent<Bullet>().offset = 0;
        bullets.GetComponent<Bullet>().player = player;
        bullets.GetComponent<Bullet>().cam = cam;
        if (powerUpActive)
        {
            bullets.GetComponent<Bullet>().powerUpActive = true;
        }
        else
        {
            bullets.GetComponent<Bullet>().powerUpActive = false;
        }
    }
}
