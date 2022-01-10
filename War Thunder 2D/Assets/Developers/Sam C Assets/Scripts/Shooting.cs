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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetButtonDown("Submit"))
        {
            PlayerShoot();
        }
        */
    }

    private void PlayerShoot()
    {
        GameObject bullets = Instantiate(bullet);
        bullets.GetComponent<Bullet>().offset = 0;
        bullets.GetComponent<Bullet>().player = player;
        bullets.GetComponent<Bullet>().cam = cam;
    }
}
