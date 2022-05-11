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
    public GameState currentGameState;

    
    public float timer = 1f;
    public float timer2 = 1f;

    public bool dead;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.rotation = Quaternion.Euler(0, 0, -90.0f);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (currentGameState == GameState.Playing)
        {
            Debug.Log("Player State");
            //timer -= Time.deltaTime;
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
        else if(currentGameState == GameState.Menu)
        {
            rb.velocity = transform.up * 0;
        }
        else
        {
            Debug.Log("Lose State");
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0.0f;
        }
    }
}
