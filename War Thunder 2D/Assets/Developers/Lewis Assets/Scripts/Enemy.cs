using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    [System.NonSerialized] 
    public EnemySpawner spawner;
    [System.NonSerialized] 
    public Camera cam;
    [System.NonSerialized] 
    public Vector2 initialVelocity;

    private Rigidbody2D rb;
    private Vector3 screenBounds;
    private float speed = 3f;
    private float enemyRange = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        checkDirection();
        spawningDirection();
    }


    private void checkDirection()
    {
        float camX = cam.transform.position.x;
        float camY = cam.transform.position.y;

        //Change to just redirect enemies to a different position once off the screen.
        //once player is off the screen rotate back towards the player with a random offset and carry on in 
        if (transform.position.x < camX - enemyRange)
        {
            // needs to trigger once?
            SetMovementDirection();
            RotateToTarget(new Vector2(rb.velocity.x, rb.velocity.y));
        }

        if (transform.position.x > camX + enemyRange)
        {
            SetMovementDirection();
            RotateToTarget(new Vector2(rb.velocity.x, rb.velocity.y));
        }

        if (transform.position.y < camY - enemyRange)
        {
            SetMovementDirection();
            RotateToTarget(new Vector2(rb.velocity.x, rb.velocity.y));
        }

        if (transform.position.y > camY + enemyRange)
        {
            SetMovementDirection();
            RotateToTarget(new Vector2(rb.velocity.x, rb.velocity.y));
        }
    }

    private void spawningDirection()
    {
        if (rb.velocity.x == 0 || rb.velocity.y == 0)
        {
            // x will always be 0 to head towards the player, y will have a random offset
            if (initialVelocity.y == 0)
            {
                //this is going to be moving from left or right
                rb.velocity = new Vector2(initialVelocity.x, Random.Range(-0.5f, 0.5f)) * speed;
                RotateToTarget(new Vector2(rb.velocity.x, rb.velocity.y));
            }
            else
            {
                //this is going to be moving from top or bottom
                rb.velocity = new Vector2(Random.Range(-0.5f, 0.5f), initialVelocity.y) * speed;
                RotateToTarget(new Vector2(rb.velocity.x, rb.velocity.y));
            }
        }
    }

    private void RotateToTarget(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void SetMovementDirection()
    {
        Vector2 camPos = new Vector2(cam.transform.position.x, cam.transform.position.y);
        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
        
        // tweak the inside unit circle variable to produce more realistic results,
        // need to slow our plane down as well and maybe speed up enemies?
        Vector2 direction = (camPos + Random.insideUnitCircle * 15 - enemyPos).normalized * speed;
        rb.velocity = direction;
    }
}