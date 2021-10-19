using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 screenBounds;
    public Camera cam;

    public float spawnDir;

    private float speed = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = new Vector2(Random.Range(-3, 3), Random.Range(-3, 3));
    }

    // Update is called once per frame
    void Update()
    {
        //Removes Enemies from game if too far away or off of the screen.
        //Change to just redirect enemies to a different position once off the screen.
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
        
        
        if (rb.velocity.x == 0 || rb.velocity.y == 0)
        {
            // x will always be 0 to head towards the player, y will have a random offset
            rb.velocity = new Vector2(spawnDir, Random.Range(-0.5f, 0.5f)) * speed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            print("HIT");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
