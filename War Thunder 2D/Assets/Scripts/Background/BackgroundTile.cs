using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTile : MonoBehaviour
{
    [Header("Powerups")] 
    public float chanceToSpawn = 0.8f;
    public GameObject[] availablePowerUps;

    [Header("Enviroment")] 
    public float treeHeight;
    public float treeOffset;
    public int minTrees;
    public int maxTrees;
    public float cloudMaxHeight;
    public float cloudMinHeight;
    public int minClouds;
    public int maxClouds;

    [Header("References")] 
    public GameObject treeObject;
    public GameObject[] cloudObject;

    [System.NonSerialized] public Vector2 tilePosition = new Vector2();
    [System.NonSerialized] public TileGenerator generator;
    [System.NonSerialized] public GameObject player;
    [System.NonSerialized] public GameObject spawner;

    private GameObject tileObject;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;
    private BoxCollider2D bounds;
    private int tileType;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = this.transform.Find("Square").GetComponent<SpriteRenderer>();
        bounds = this.transform.Find("Square").GetComponent<BoxCollider2D>();
        _collider = GetComponent<BoxCollider2D>();
        _collider.enabled = false;
        this.gameObject.name = "Tile - " + tilePosition.y;
        getTileType();

        // sererate the spawn power ups into a different function that can be called from the event manager?
        if (tileType == 3)
        {
            SpawnPowerups();
        }
    }

    void getTileType()
    {
        if (tilePosition.y < 0)
        {
            //dirt
            setTile(0);
            tileType = 0;
        }
        else if (tilePosition.y == 0)
        {
            //ground
            setTile(1);
            tileType = 1;
        }
        else if (tilePosition.y > 0 && tilePosition.y < 60)
        {
            //sky
            setTile(2);
            tileType = 2;
        }
        else if (tilePosition.y == 60)
        {
            //transition
            setTile(3);
            tileType = 3;
        }
        else
        {
            //space
            setTile(4);
            tileType = 4;
        }
    }

    void setTile(int layer)
    {
        switch (layer)
        {
            case 0:
                _spriteRenderer.sprite = generator.tileTypes[0].sprite;
                break;
            case 1:
                _spriteRenderer.sprite = generator.tileTypes[1].sprite;
                SpawnTrees();
                _collider.enabled = true;
                break;
            case 2:
                _spriteRenderer.sprite = generator.tileTypes[2].sprite;
                SpawnClouds();
                break;
            case 3:
                _spriteRenderer.sprite = generator.tileTypes[3].sprite;
                //transition
                SpawnClouds();
                break;
            case 4:
                _spriteRenderer.sprite = generator.tileTypes[4].sprite;
                //spawnStars();
                break;
        }
    }

    void SpawnPowerups()
    {
        foreach (var powerUp in availablePowerUps)
        {
            //%20 percent chance (1 - 0.8 is 0.2)
            if (Random.value > chanceToSpawn)
            {
                if (powerUp.tag == "Heart")
                {
                    GameObject heart = Instantiate(powerUp, GetRandomLocationInRange(), Quaternion.Euler(0, 0, 0));
                    heart.GetComponent<HealthPowerup>().health = player.GetComponent<PlayerHealth>();
                    heart.transform.SetParent(this.transform);
                }
                else if (powerUp.tag == "Coin")
                {
                    GameObject coin = Instantiate(powerUp, GetRandomLocationInRange(), Quaternion.Euler(0, 0, 0));
                    coin.transform.SetParent(this.transform);
                }
                else if (powerUp.tag == "Double")
                {
                    GameObject doublePoints =
                        Instantiate(powerUp, GetRandomLocationInRange(), Quaternion.Euler(0, 0, 0));
                    doublePoints.transform.SetParent(this.transform);
                }
            }
        }
    }

    Vector3 GetRandomLocationInRange()
    {
        Vector3 position = new Vector3(tilePosition.x + 15f + Random.insideUnitCircle.x * Random.Range(5f, 25f),
            tilePosition.y + 15f + Random.insideUnitCircle.y * Random.Range(5f, 25f), 0f);
        return position;
    }

    Vector3 GetRandomPointInBounds()
    {
        return new Vector3(
            Random.Range(bounds.bounds.min.x, bounds.bounds.max.x),
            Random.Range(bounds.bounds.min.y, bounds.bounds.max.y),
            Random.Range(bounds.bounds.min.z, bounds.bounds.max.z)
            );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(collision.gameObject);
        }
        else if(collision.tag == "Enemy")
        {
            spawner.GetComponent<EnemySpawner>().EnemyDestroyed();
            collision.gameObject.GetComponent<Enemy>().MyOwnDestroy();
        }
        
        // need something for powerups here as well..
    }
    
    private void SpawnTrees()
    {
        float startHeight = treeHeight;
        int treeAmount = Random.Range(minTrees, maxTrees);
        float treeDistance = 26 / treeAmount;

        for (int a = 1; a <= treeAmount + 1; a++)
        {
            float posY = startHeight + Random.Range(3, 6);
            float posX = tilePosition.x + treeDistance * a + Random.Range(1, 5);
            Vector3 treePosition =  new Vector3(posX,posY,0);
            GameObject tree = Instantiate(treeObject, treePosition, Quaternion.Euler(0, 0, 0));
            tree.transform.SetParent(this.transform);
        }
    }
    
    private void SpawnClouds()
    {
        int cloudAmount = Random.Range(minClouds, maxClouds);
        for (int i = 0; i < cloudAmount; i++)
        {
            GameObject chosenCloud = cloudObject[Random.Range(0, cloudObject.Length)];
            //get a random cloud in the cloud array to spawn;
            GameObject cloud = Instantiate(chosenCloud, GetRandomPointInBounds(), Quaternion.Euler(0, 0, 0));
            cloud.transform.SetParent(this.transform);
        }
    }
}

[System.Serializable]
public class TileType
{
    public string tileName;
    public int layer;
    public Sprite sprite;
}


