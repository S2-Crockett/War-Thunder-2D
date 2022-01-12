using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float rotSpeed;
    public float speed;
    private Rigidbody2D rb;
    public Vector3 eulerAngles;
    // Start is called before the first frame update
    public float timer = 1f;
    public float timer2 = 1f;

    public bool dead;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   

    // Update is called once per frame
    void Update()
    {
        dead = GetComponent<PlayerHealth>().die;
        if (!dead)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                rb.velocity = transform.up * 0;
                timer2 -= Time.deltaTime;
                if (timer2 <= 0)
                {
                    timer = 0.02f;
                    timer2 = 0.02f;
                }
            }
            else
            {
                rb.velocity = (transform.up * speed);
                float angle = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg;
                if (angle != 0)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, -angle),
                        Time.deltaTime * rotSpeed);
                }
            }
        }
        else
        {
            rb.velocity = transform.up * 0;
        }
    }
}
