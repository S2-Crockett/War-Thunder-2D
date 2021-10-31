using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTile : MonoBehaviour
{
    public float chanceToSpawn = 0.8f;
    public GameObject[] availablePowerUps;

    [System.NonSerialized] public Vector2 tilePosition = new Vector2();

    [System.NonSerialized] public TileGenerator generator;

    [System.NonSerialized] public GameObject player;

    private GameObject tileObject;
    private SpriteRenderer _spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = this.transform.Find("Square").GetComponent<SpriteRenderer>();
        this.gameObject.name = "Tile - " + tilePosition.y;
        getTileType();
        SpawnPowerups();
    }

    void getTileType()
    {
        if (tilePosition.y < 0)
        {
            //dirt
            setTile(0);
        }
        else if (tilePosition.y == 0)
        {
            //ground
            setTile(1);
        }
        else if (tilePosition.y > 0 && tilePosition.y < 120)
        {
            //sky
            setTile(2);
        }
        else if (tilePosition.y == 120)
        {
            //transition
            setTile(3);
        }
        else
        {
            //space
            setTile(4);
        }
    }

    // Update is called once per frame
    void Update()
    {

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
                break;
            case 2:
                _spriteRenderer.sprite = generator.tileTypes[2].sprite;
                break;
            case 3:
                _spriteRenderer.sprite = generator.tileTypes[3].sprite;
                break;
            case 4:
                _spriteRenderer.sprite = generator.tileTypes[4].sprite;
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
                    GameObject heart = Instantiate(powerUp, GetRandomLocationInRange(), Quaternion.Euler(0,0,0));
                    heart.GetComponent<HealthPowerup>().health = player.GetComponent<PlayerHealth>();
                    heart.transform.SetParent(this.transform);
                }
                else if (powerUp.tag == "Coin")
                {
                    GameObject coin = Instantiate(powerUp, GetRandomLocationInRange(), Quaternion.Euler(0,0,0));
                    coin.GetComponent<CoinPowerup>().score = player.GetComponent<ScoreScript>();
                    coin.transform.SetParent(this.transform);
                }
            }
        }

        Vector3 GetRandomLocationInRange()
        {
            Vector3 position = new Vector3(tilePosition.x + 15f + Random.insideUnitCircle.x * Random.Range(5f, 25f),
                tilePosition.y + 15f + Random.insideUnitCircle.y * Random.Range(5f, 25f), 0f);
            return position;
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


