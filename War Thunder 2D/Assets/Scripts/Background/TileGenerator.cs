using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject player;
    public Transform playerTransform;
    public GameObject baseTile;
    
    public int tileRenderRange = 2;
        
    public TileType[] tileTypes;

    private int tileSize = 30;
    private int tileX;
    private int tileY;
    
    List<GameObject> tiles = new List<GameObject>();
    List<Vector2> tileCoordinates = new List<Vector2>();
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerPosition();
        AddTiles(); 
        RemoveTiles();
    }

    bool RadiusCheck(float x, float y)
    {
        Vector3 tile = new Vector3(x,y);
        return Vector2.Distance(tile, playerTransform.position) < tileSize * tileRenderRange;
    }

    void UpdatePlayerPosition()
    {
        int x = Mathf.FloorToInt(playerTransform.position.x / 30f);
        int y = Mathf.FloorToInt(playerTransform.position.y / 30f);

        if (x != tileX || y != tileY)
        {
            tileX = x;
            tileY = y;
        }
    }

    void AddTiles()
    {
        for (int x = tileRenderRange * -1; x < tileRenderRange - 1; x++)
        {
            int tileGlobalX = x + tileX;
            
            for (int y = tileRenderRange * -1; y < tileRenderRange - 1; y++)
            {
                int xa = x + tileX;
                int xb = xa * tileSize;
                int xtotal = xb + 30;
                
                int ya = y + tileY;
                int yb = ya * tileSize;
                int ytotal = yb + 30;

                int tileGlobalY = y + tileY;
                
                Vector2 tileSpawn = new Vector2((float)xtotal, (float)ytotal);
                
                if (RadiusCheck((float) xtotal, (float) ytotal))
                {
                    if (!tileCoordinates.Contains(new Vector2((float)tileGlobalX, (float) tileGlobalY)))
                    {
                        
                        // if it doesn't contain then spawn
                        tileCoordinates.Add(new Vector2((float)tileGlobalX, (float) tileGlobalY));

                        GameObject tile = Instantiate(baseTile, tileSpawn, new Quaternion(0f, 0f, 0f, 0f));
                        tile.GetComponentInChildren<BackgroundTile>().tilePosition = tileSpawn;
                        tile.GetComponentInChildren<BackgroundTile>().generator = this;
                        tile.GetComponentInChildren<BackgroundTile>().player = player;
                        tile.transform.SetParent(transform);
                        tiles.Add(tile);
                    }
                }
            }
        }
    }

    void RemoveTiles()
    {
        for (int i = 0; i < tileCoordinates.Count; i++)
        {
            float x = tileSize * tileCoordinates[i].x + 30f;
            float y = tileSize * tileCoordinates[i].y + 30f;

            if (!RadiusCheck(x, y))
            {
                Destroy(tiles[i].gameObject);
                tiles.RemoveAt(i);
                tileCoordinates.RemoveAt(i);
            }
        }

    }
}
