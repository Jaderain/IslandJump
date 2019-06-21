using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [System.Serializable]
    public class SpawnLocAndRotation
    {
        public Vector3 spawnPivot;
        public float beamLength;
        public Vector3 rotation;
    }

    // island Setup
    public GameObject islandObject;
    public Vector3[] islandSpawns;

    // coin spawn Setup
    public GameObject coinObject;
    public Vector3 coinOffset;
    public float coinRespawnCooldown;

    // projectile Setup
    public bool projectileSpawnEnabled;
    public GameObject hazardRespawnObject;
    public GameObject projectileObject;
    public float projectileRespawnCooldown;
    public SpawnLocAndRotation[] projectileSpawnLoc;

    // status properties
    public bool gameOverProperty { get; private set; }

    // in game scores and stuff
    private int score;
    private int lastCoinIndex;

    // other game objects
    private Text scoreText;
    private Text gameOverText;

    void Start()
    {
        // Use this for initialization
        score = 0;
        lastCoinIndex = 0;
        gameOverProperty = false;

        // init platforms
        initPlatforms();

        // call all gameobject needed
        // call UI and init the values
        var canvasItem = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
        var textItems = canvasItem.GetComponentsInChildren<Text>();

        foreach (var item in textItems)
        {
            if (item.name == "ScoreText")
            {
                scoreText = item;
                scoreText.text = "0";
            }
            else if (item.name == "GameOverText")
            {
                gameOverText = item;
                gameOverText.enabled = false;
            }
        }

        // spawn coins
        // TODO: move to game start group? seperate init and game start?
        Debug.Log("Game Started, Init done");
        StartCoroutine(spawnCoin());
    }

    // Update is called once per frame
    void Update()
    {
        // spawn coin every once in awhile on the platform
    }

    void initPlatforms()
    {
        // create platform on each vector 3 inputs
        foreach (Vector3 item in islandSpawns)
        {
            Instantiate(islandObject, item, Quaternion.Euler(new Vector3(0, 45, 0)));
        }

        // TODO: move this projectile enabled to each individual spawns
        if (projectileSpawnEnabled)
        {
            // TODO: all projectile spawn at ONCE - should be spawned one at a time or less than 4
            // create hazard respawn points
            foreach (var item in projectileSpawnLoc)
            {
                var spawnedItem = Instantiate(hazardRespawnObject, item.spawnPivot, Quaternion.Euler(item.rotation));

                // set script variables
                spawnedItem.GetComponent<SpawnProjectiles>().beamLength = item.beamLength;
                spawnedItem.GetComponent<SpawnProjectiles>().projectileObject = projectileObject;
                spawnedItem.GetComponent<SpawnProjectiles>().projectileRespawnCooldown = projectileRespawnCooldown;
            }
        }
    }
    
    IEnumerator spawnCoin()
    {
        Debug.Log("Spawning Coin Called");

        // spawn coins based on island location, based on rates
        yield return new WaitForSeconds(coinRespawnCooldown);
        lastCoinIndex = (lastCoinIndex + Random.Range(1, islandSpawns.Length)) % islandSpawns.Length;
        
        Debug.Log("Wait Done - Creating Coin on Index[" + lastCoinIndex + "]");
        
        Instantiate(coinObject, islandSpawns[lastCoinIndex] + coinOffset, coinObject.transform.rotation);
    }

    /* public method to be called from other scripts for updates */
    public void spawnPlayer()
    {
        // TODO: SPAWN PLAYER instead start with default asset
    }

    public void gainCoin(int scoreValue)
    {
        score += scoreValue;
        scoreText.text = score.ToString();

        Debug.Log("Coin Gained. CurrentScore[" + score + "]");

        StartCoroutine(spawnCoin());
    }
    
    public void gameOverNow()
    {
        // display game over text
        gameOverProperty = true;
        gameOverText.enabled = true;

        // Allow restart of the game
        // TODO: finish game over screen?
    }
}
