using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;

    public GameObject player;
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
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       Debug.Log("Collided"); 
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(this);
            Destroy(collision.gameObject);
        }
    }
}
