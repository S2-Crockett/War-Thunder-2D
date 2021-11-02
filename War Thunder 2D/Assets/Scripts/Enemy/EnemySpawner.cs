using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // public variables
    public GameObject enemyPlane;
    public GameObject enemyBomber;
    public Transform player;

    public int numPlanesSpawning;
    public int numPlanesMax;
    public int numWaves;
    public float respawningDelay;
    public int enemySpawnOffset = 25;

    public ScoreScript scorescript;
    public NextLevel levelscript;

    float bomberChance = 0.9f;
    float bomberTimer = 5f;

    int index;


    // private variables
    private Vector2[] spawnPositions = new[]
    {
        new Vector2(0f, 0f), //left
        new Vector2(0f, 0f), //top
        new Vector2(0f, 0f), //right
        new Vector2(0f, 0f), //bottom
        new Vector2(0f, 0f), //bottom
        new Vector2(0f, 0f) //bottom
    };

    private Camera cam;
    private bool gameStarted = false;
    private int planesDestroyed = 0;
    private int planesSpawned = 0;
    private int currentWave = 0;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        StartCoroutine(StartSpawning());
    }

    // Update is called once per frame
    void Update()
    {
        // updates spawn positions to keep inline with the cameras
        float camX = cam.transform.position.x;
        float camY = cam.transform.position.y;

        spawnPositions[0] = new Vector3(camX - enemySpawnOffset, camY, 9); // update left position
        spawnPositions[1] = new Vector3(camX, camY + enemySpawnOffset, 9); // update top position
        spawnPositions[2] = new Vector3(camX + enemySpawnOffset, camY, 9); // update right position
        spawnPositions[3] = new Vector3(camX, camY - enemySpawnOffset, 9); // update bottom position

        spawnPositions[4] = new Vector3(camX - enemySpawnOffset, camY + 10, 9);
        spawnPositions[5] = new Vector3(camX + enemySpawnOffset, camY + 10, 9);

        bomberTimer -= 1.0f * Time.deltaTime;
        if(bomberTimer <= 0)
        {
            if(Random.value > bomberChance)
            {
                print("Spawn");             
                SpawnBomber();
                bomberTimer = 5.0f;
            }
        }



    }

    IEnumerator StartSpawning()
    {
        gameStarted = true;
        while (gameStarted)
        {
            // spawn initial wave of enemies - decrease planes spawned to spawn more.
            if (planesSpawned < numPlanesSpawning && planesDestroyed + planesSpawned < numPlanesMax)
            {
                yield return new WaitForSeconds(respawningDelay);
                SpawnPlane();
                planesSpawned++;
            }

            yield return new WaitForSeconds(1);
        }
    }

    // get randomised location to spawn the enemy will need to account the player height when doing
    // work with the procedural background so enemies dont spawn from the land of spawn from space (if you go
    // high enough)
    void SpawnPlane()
    {
        // spawn a plane and set all the default components in the enemy class
        GameObject plane = Instantiate(enemyPlane) as GameObject;
        plane.GetComponent<Enemy>().cam = cam;
        plane.GetComponent<Enemy>().spawner = this;

        // choose a random location , based off of the players height and spawn them
        int index = Random.Range(0, spawnPositions.Length);

        plane.transform.position = spawnPositions[index];

        if (player.position.y < 15)
        {
            index = Random.Range(0, 2);
            plane.transform.position = spawnPositions[index];
        }
        else
        {
            index = Random.Range(0, 3);
            plane.transform.position = spawnPositions[index];
        }

        if (index == 0)
        {
            //left
            plane.GetComponent<Enemy>().initialVelocity = new Vector2(1,0);
        }
        else if (index == 1)
        {
            //top
            plane.GetComponent<Enemy>().initialVelocity = new Vector2(0,-1);
        }
        else if (index == 2)
        {
            //right
            plane.GetComponent<Enemy>().initialVelocity = new Vector2(-1,0);
        }
        else if (index == 3)
        {
 
            plane.GetComponent<Enemy>().initialVelocity = new Vector2(0, 1);
       
        }
    }

    void SpawnBomber()
    {
        GameObject bomber = Instantiate(enemyBomber) as GameObject;
        bomber.GetComponent<EnemyBomber>().cam = cam;
        bomber.GetComponent<EnemyBomber>().spawner = this;

        int index = Random.Range(4, 5);
        bomber.transform.position = spawnPositions[index];
        if (index == 4)
        {
            //left
            bomber.GetComponent<EnemyBomber>().initialVelocity = new Vector2(1, 0);
        }
        else if (index == 5)
        {
            //right
            bomber.GetComponent<EnemyBomber>().initialVelocity = new Vector2(-1, 0);
        }
    }

    // called from the enemy when it gets destroyed to determine if we need to spawn another enemy to replace it.
    public void EnemyDestroyed()
    {
        planesDestroyed++;

        scorescript.AddScore(100);

        if (planesDestroyed < numPlanesMax)
        {
            planesSpawned--;
        }
        else
        {
            if (currentWave != numWaves)
            {
                planesDestroyed = 0;
                currentWave++;
            }
            else
            {
                // GO TO NEXT LEVEL
                levelscript.LevelNum++;
                numWaves = 1;
                Debug.Log("Level increased");
               
            }
        }
    }
}