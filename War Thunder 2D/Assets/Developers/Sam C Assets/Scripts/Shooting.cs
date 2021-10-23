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
    private float timer = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button1))
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
        if (timer <= 0)
        {
            GameObject bullets = Instantiate(bullet);
            bullets.GetComponent<Bullet>().player = player;
            bullets.GetComponent<Bullet>().cam = cam;
            timer = 0.5f;
        }
        else
            timer -= Time.deltaTime;
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
            if(collision.gameObject.tag == "Player")
            {
                EnemyShoot();
            }
    }

}
