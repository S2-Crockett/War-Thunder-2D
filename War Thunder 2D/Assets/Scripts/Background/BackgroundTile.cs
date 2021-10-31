using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTile : MonoBehaviour
{
    private TileCoord coordinates;
    private BackgroundGenerator generator;

    private GameObject tileObject;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

