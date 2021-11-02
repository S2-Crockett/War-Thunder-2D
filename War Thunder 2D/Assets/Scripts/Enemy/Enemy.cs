using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 6.5f;
    public float enemyRange = 15f;

    [Header("Behaviour")] 
    public float viewDistance = 4.3f;
    public float shootDelay = 0.5f;

    [Header("Extras")]
    public GameObject bullets;
    
    [System.NonSerialized] 
    public EnemySpawner spawner;
    [System.NonSerialized] 
    public Camera cam;
    [System.NonSerialized] 
    public Vector2 initialVelocity;

    private Rigidbody2D rb;
    private Vector3 screenBounds;

    private bool follow = false;
    private float followTimer = 4.0f;
    private float bomberTimer = 1.0f;
    private Transform Target;
    private float RotationSpeed = 2.0f;
    private float distance;

    private Quaternion lookRotation;
    private Vector3 direction;
    private int direction_;

    RaycastHit2D hit;

    private bool setDir = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, Target.transform.position);

        if (gameObject.tag == "Enemy")
        {
            checkDirection();
            spawningDirection();
            checkForPlayer();
            chasePlayer();
        }
        if(gameObject.tag == "EnemyBomber")
        {
            spawningDirectionBomber();
            BomberFire();
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
        }
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
            followTimer = 4.0f;
        }
        if (transform.position.x > camX + enemyRange)
        {
            SetMovementDirection();
            RotateToTarget(new Vector2(rb.velocity.x, rb.velocity.y));
            followTimer = 4.0f;
        }
        if (transform.position.y < camY - enemyRange)
        {
            SetMovementDirection();
            RotateToTarget(new Vector2(rb.velocity.x, rb.velocity.y));
            followTimer = 4.0f;
        }
        if (transform.position.y > camY + enemyRange)
        {
            SetMovementDirection();
            RotateToTarget(new Vector2(rb.velocity.x, rb.velocity.y));
            followTimer = 4.0f;
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
            
            //lerp from old movement rotation to the new one
            // NEED A SMOOTHER TRANSITION / ROTATION TO SLOWLY ROTATE
        }
    }

    private void SetMovementDirection()
    {
        Vector2 camPos = new Vector2(cam.transform.position.x, cam.transform.position.y);
        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
        
        //lerp from current movement direction to new movement direction
        
        // tweak the inside unit circle variable to produce more realistic results,
        // need to slow our plane down as well and maybe speed up enemies?
        Vector2 direction = (camPos + Random.insideUnitCircle * 3 - enemyPos).normalized * speed;
        rb.velocity = direction;
    }

    private void checkForPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), viewDistance, 11);
        if (hit.collider != null && hit.collider.tag == "Player")
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * viewDistance, Color.yellow);
            shoot();
            if (distance <= 10.0f)
            {
                follow = true;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * viewDistance, Color.white);
        }
    }

    private void BomberFire()
    {
        bomberTimer -= 1.0f * Time.deltaTime;
        if(bomberTimer <= 0)
        {
            shootBomb();
            bomberTimer = 1.0f;
        }
    }

    private void chasePlayer()
    {
        direction = Target.position - transform.position;
        float angle = math.atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        if (follow)
        {
            followTimer -= 1.0f * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation,  q, Time.deltaTime * RotationSpeed);
            rb.velocity = transform.up * speed;
        }
        if (followTimer <= 0)
        {
            follow = false;
        }
    }

    private void shoot()
    {
        if (shootDelay <= 0)
            {       
            GameObject bullet = Instantiate(bullets);
            bullets.GetComponent<Bullet>().offset = 0;
            bullets.GetComponent<Bullet>().player = this.gameObject;
            bullets.GetComponent<Bullet>().cam = cam;
            shootDelay = 0.5f;
            }
            else
            {
                shootDelay -= Time.deltaTime;
            }
    }

    private void shootBomb()
    {
            if (direction_ == 1)
            {
                GameObject bullet = Instantiate(bullets);
                bullets.GetComponent<Bullet>().offset = 90;
            }
            if (direction_ == 2)
            {
                GameObject bullet = Instantiate(bullets);
                bullets.GetComponent<Bullet>().offset = -90;
            }
            bullets.GetComponent<Bullet>().player = this.gameObject;
            bullets.GetComponent<Bullet>().cam = cam;
    }

    private void DestroyEnemy()
    {
        //get rid of the sprite,

        //then have a slight delay and spawn new sprite

        Destroy(this);
    }
}