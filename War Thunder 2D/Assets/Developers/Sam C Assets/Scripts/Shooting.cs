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
            PlayerShoot();
        }
    }

    private void PlayerShoot()
    {
        GameObject bullets = Instantiate(bullet);
        bullets.GetComponent<Bullet>().player = player;
        bullets.GetComponent<Bullet>().cam = cam;
    }
}
