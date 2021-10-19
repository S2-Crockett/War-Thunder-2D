using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;

    public GameObject player;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.rotation = player.transform.rotation;
        transform.position = player.transform.position;
        rb.velocity = transform.up * 40;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < cam.transform.position.x - 10)
        {
            Destroy(this.gameObject);
            // once player is off the screen rotate back towards the player with a random offset and carry on in 
            // that direction
        }
        if (transform.position.x > cam.transform.position.x + 10)
        {
            Destroy(this.gameObject);
        }
        if (transform.position.y < cam.transform.position.y - 8)
        {
            Destroy(this.gameObject);
        }
        if (transform.position.y > cam.transform.position.y + 8)
        {
            Destroy(this.gameObject);
        }

    }


}

