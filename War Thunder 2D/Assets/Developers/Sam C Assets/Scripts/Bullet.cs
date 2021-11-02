using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{

    public EnemySpawner spawner;

    private Rigidbody2D rb;
    public float speed;

    public GameObject player;
    public Camera cam;

    public PlayerHealth health;

    public int offset = 0;
    private Transform playerTran;

    private SpriteRenderer spriterenderer;
    public Sprite sprite;
    private float SpriteTimer = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        if(this != null)
        {
            rb = GetComponent<Rigidbody2D>();
            playerTran = player.transform;

            if (player != null)
            {
                transform.rotation = new Quaternion(playerTran.rotation.x, playerTran.rotation.y, playerTran.rotation.z + offset, playerTran.rotation.w);
                transform.position = player.transform.position;
                health = player.GetComponent<PlayerHealth>();
            }

            rb.velocity = transform.up * 20;
            spawner = GameObject.Find("Enemy Spawner").GetComponent<EnemySpawner>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < cam.transform.position.x - 15)
        {
            Destroy(this.gameObject);
            // once player is off the screen rotate back towards the player with a random offset and carry on in 
            // that direction
        }
        if (transform.position.x > cam.transform.position.x + 15)
        {
            Destroy(this.gameObject);
        }
        if (transform.position.y < cam.transform.position.y - 12)
        {
            Destroy(this.gameObject);
        }
        if (transform.position.y > cam.transform.position.y + 12)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBomber") && player.gameObject.tag == "Player")
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Enemy>().MyOwnDestroy();
                spawner.EnemyDestroyed();
            }
            if (collision.gameObject.tag == "EnemyBomber")
            {
                collision.gameObject.GetComponent<EnemyBomber>().MyOwnDestroy();
            }
        
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player" && (player.gameObject.tag == "Enemy" || player.gameObject.tag == "EnemyBomber"))
        {
            Destroy(gameObject);

        }
    }

}