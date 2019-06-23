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

    // player prefab
    public GameObject playerPrefab;

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

    // accessible properties
    public bool gameOverProperty { get; private set; }
    public PlayerController currentPlayerContProperty { get; private set; }
    
    // in game scores and stuff
    private int score;
    private int lastCoinIndex;

    // in game other UI
    private Text scoreText;
    private Text gameOverText;

    void Start()
    {
        // Use this for initialization
        currentPlayerContProperty = null;
        score = 0;
        lastCoinIndex = 0;
        gameOverProperty = false;

        // Stage Initialization here
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
        Debug.Log("Game Started, Init done");
        StartCoroutine(spawnCoin());

        // player start now
        // TODO: Make Game start instead? press button to start the game instead
        // TODO: Enviornment setup at the start 
        // TODO: donce it is done, show that the game is ready to start (return point here after game over)
        playerStart();
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
        // TODO: Balance - Coin sometimes spawns TOO close to player, need to spawn to other side of the player
        Debug.Log("Spawning Coin Called");

        // spawn coins based on island location, based on rates
        yield return new WaitForSeconds(coinRespawnCooldown);
        lastCoinIndex = (lastCoinIndex + Random.Range(1, islandSpawns.Length)) % islandSpawns.Length;
        
        Debug.Log("Wait Done - Creating Coin on Index[" + lastCoinIndex + "]");
        
        Instantiate(coinObject, islandSpawns[lastCoinIndex] + coinOffset, coinObject.transform.rotation);
    }

    /* public method to be called from other scripts for updates */
    public void playerStart()
    {
        // Spawn a player 
        if (currentPlayerContProperty == null)
        {
            // reset values
            score = 0;
            scoreText.text = "0";
            gameOverProperty = false;
            gameOverText.enabled = false;

            // spawn a plyaer
            var player = Instantiate(playerPrefab, new Vector3(0, 1, 3), Quaternion.Euler(new Vector3(0, 45, 0)));
            currentPlayerContProperty = player.GetComponent<PlayerController>();
        }
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

        // TODO: check to see if the game is ready to be restarted, delay needed for each restart
    }
}
