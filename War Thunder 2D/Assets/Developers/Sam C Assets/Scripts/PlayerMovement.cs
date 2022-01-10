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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.rotation = Quaternion.Euler(0, 0, -90.0f);
    }
    
    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.up * speed;
        if (currentGameState == GameState.Playing)
        {
            float angle = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg;
            if (angle != 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, -angle), Time.deltaTime * rotSpeed);
            }
        }
    }
}
