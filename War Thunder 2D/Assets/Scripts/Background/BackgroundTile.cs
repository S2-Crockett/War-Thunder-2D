using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTile : MonoBehaviour
{
    [System.NonSerialized] 
    public Vector2 tilePosition = new Vector2();

    [System.NonSerialized] 
    public TileGenerator generator;
    
    private GameObject tileObject;
    
    private SpriteRenderer _spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        this.transform.parent.gameObject.name = "Tile - " + tilePosition.y;
        getTileType();
    }
    
    private void Awake()
    {
        
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
        else if(tilePosition.y > 0 && tilePosition.y < 120)
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
        {   //space
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
            case 0 :
                _spriteRenderer.sprite = generator.tileTypes[0].sprite;
                break;
            case 1 :
                _spriteRenderer.sprite = generator.tileTypes[1].sprite;
                break;
            case 2 :
                _spriteRenderer.sprite = generator.tileTypes[2].sprite;
                break;
            case 3 :
                _spriteRenderer.sprite = generator.tileTypes[3].sprite;
                break;
            case 4 :
                _spriteRenderer.sprite = generator.tileTypes[4].sprite;
                break;
        }
    }
}

// allow each tile to keep track of their coordinates on the map
public class TileCoord
{
    public int x;
    public int y;

    public TileCoord(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
}

[System.Serializable]
public class TileType
{
    public string tileName;
    public int layer;
    public Sprite sprite;
}


