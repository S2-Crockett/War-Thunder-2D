using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class BackgroundGenerator : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject baseTile;
    public int WorldSizeInTiles = 0;
    public int WorldViewDistance = 1;
    
    TileCoord playerTileCoord;
    TileCoord playerLastTileCoord;

    private Vector2 spawnPosition;
    [CanBeNull] List<GameObject> tiles = new List<GameObject>();
    List<TileCoord> newTiles = new List<TileCoord>();
    List<TileCoord> activeTiles = new List<TileCoord>();
    
    // Start is called before the first frame update
    void Start()
    {
        playerLastTileCoord = GetCoordFromVector2(playerTransform.position);
        //spawnPosition = new Vector2((WorldSizeInTiles * 30) / 2f,
        //    (WorldSizeInTiles * 30) / 2f);
        //playerTransform.position = spawnPosition;
        GenerateInitialTiles();
    }

    // Update is called once per frame
    void Update()
    {
        // update the current player coordinate 
        playerTileCoord = GetCoordFromVector2(playerTransform.position);
        
        // if the coordinates are different then check the distance to see
        // if we need to spawn more tiles in
        if (playerTileCoord.x == playerLastTileCoord.x && playerTileCoord.y == playerLastTileCoord.y)
        {
            
        }
        else
        {
            // we have changed coordinates, check if we have new coordinates based on our distance
            playerLastTileCoord = GetCoordFromVector2(playerTransform.position);
            CheckDistance();
            DestroyTileOutsideRange();
        }
    }

    void CheckDistance()
    {
        // check our distance to see if we need to spawn any more tiles or remove them?
        TileCoord coord = GetCoordFromVector2(playerTransform.position);

        for (int x = coord.x - WorldViewDistance; x < coord.x + WorldViewDistance; x++)
        {
            for (int y = coord.y - WorldViewDistance; y < coord.y + WorldViewDistance; y++)
            {
                if (!activeTiles.Contains(new TileCoord(x * 30, y * 30)))
                { 
                    SpawnTile(x,y);
                }
            }
        }

    }

    void GenerateInitialTiles()
    {
        for (int x = (WorldSizeInTiles / 2) - WorldViewDistance; 
            x < (WorldSizeInTiles / 2) + WorldViewDistance; x++)
        {
            for (int y = (WorldSizeInTiles / 2) - WorldViewDistance; 
                y < (WorldSizeInTiles / 2) + WorldViewDistance; y++)
                
            {
                SpawnTile(x, y);
            }
        }
    }

    void SpawnTile(int x, int y)
    {
        //spawns the tile and sets its position in the world
        Vector3 spawnPosition = new Vector3(x * 30f,y * 30f,0);
        //add tile to the new tile list
        newTiles.Add(new TileCoord(x * 30,y * 30));

        if (!activeTiles.Contains(new TileCoord(x * 30, y * 30)))
        {
            //add to array of tiles
            activeTiles.Add(new TileCoord(x * 30,y * 30));
            tiles.Add(Instantiate(baseTile, spawnPosition, new Quaternion(0f,0f,0f, 0f)));
            //adds the tile to a list of active chunks
        }
    }
    
    TileCoord GetCoordFromVector2(Vector2 pos)
    {
        int x = Mathf.FloorToInt(pos.x / 30f);
        int y = Mathf.FloorToInt(pos.y / 30f);
        return new TileCoord(x, y);
    }

    void DestroyTileOutsideRange()
    {
        foreach (var tile in activeTiles)
        {
            int x = tile.x * 30;
            int y = tile.y * 30;
            Vector2 coords = new Vector2(x,y);
            Vector2 player = new Vector2(playerTransform.position.x, playerTransform.position.y);

            int renderRange = 30 * WorldViewDistance;
            if (Vector2.Distance(coords, player) > renderRange)
            {
                print("this tile is out of range " + coords);
            }
        }
    }
}

