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
    private float timer = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit"))
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.R))
            {
                PlayerShoot();
            }
        }
    }

    private void PlayerShoot()
    {
        GameObject bullets = Instantiate(bullet);
        bullets.GetComponent<Bullet>().player = player;
        bullets.GetComponent<Bullet>().cam = cam;
    }

    private void EnemyShoot()
    {
            GameObject bullets = Instantiate(bullet);
            bullets.GetComponent<Bullet>().player = player;
            bullets.GetComponent<Bullet>().cam = cam;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
            if(collision.gameObject.tag == "Player")
            {
            if (timer <= 0)
            {
                timer = 2.0f;
                EnemyShoot();
            }
            else
                timer -= Time.deltaTime;
        }
    }

}
