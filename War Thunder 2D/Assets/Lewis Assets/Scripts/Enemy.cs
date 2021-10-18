using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Random.Range(-3, 3), Random.Range(-3, 3));
        //rb.transform.rotation = Quaternion.LookRotation(rb.velocity);
        
        //screenBounds = cam.ScreenToViewportPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));

    }

    // Update is called once per frame
    void Update()
    {
        /*
        //Removes Enemies from game if too far away.
        if (transform.position.x < -screenBounds.x * 10)
        {
            Destroy(this.gameObject);
        }
        if (transform.position.x > screenBounds.x * 10)
        {
            Destroy(this.gameObject);
        }
        if (transform.position.y < -screenBounds.y * 6)
        {
            Destroy(this.gameObject);
        }
        if (transform.position.y > screenBounds.y * 6)
        {
            Destroy(this.gameObject);
        }
        */

       
        //Changes velocity if set to zero to combat unmoving objects
        if (rb.velocity.x == 0 && rb.velocity.y == 0)
        {
            rb.velocity = new Vector2(Random.Range(-2, 2), Random.Range(-2, 2));
        }
        if (rb.velocity.x == 0)
        {
            rb.velocity = new Vector2(Random.Range(-2, 2), rb.velocity.y);
        }
        if (rb.velocity.y == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, Random.Range(-2, 2));
        }
    }
}
