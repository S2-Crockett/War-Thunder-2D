using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public GameObject bullet;
    public GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullets = Instantiate(bullet);
        bullets.GetComponent<Bullet>().player = player;
    }
}
