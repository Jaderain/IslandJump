using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    
    // player prefab
    public GameObject playerPrefab;
    
    // coin spawn Setup
    public GameObject coinObject;
    public Vector3 coinOffset;
    public float coinRespawnCooldown;
    
    // accessible properties
    public int numOfCoins { get; private set; }
    public bool gameReadyToStart { get; private set; }
    public bool gameOverProperty { get; private set; }
    public PlayerController currentPlayerContProperty { get; private set; }
    
    // in game scores and stuff
    private int score;

    // in game other UI
    private Text scoreText;
    private Text gameOverText;
   
    void Start()
    {
        // Use this for initialization
        currentPlayerContProperty = null;
        score = 0;
        gameOverProperty = false;
        gameReadyToStart = false;

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
        // TODO : move to islandScroller
        // start corutines
        StartCoroutine(spawnCoin());
        //  TODO: StartCoroutine(spawnProjectiles());
    }

    IEnumerator spawnCoin()
    {
        while (true)
        {
            if (gameReadyToStart && numOfCoins == 0)
            {
                Debug.Log("Spawning Coin Called");

                /* TODO
                // pick a location that is not the destination and the one right before
                int[] excludeThis = { currentPlayerContProperty.currentDestinationIndex, currentPlayerContProperty.nextDestinationIndex };
                int nextCoinIndex = Tools.randomNumberExcept(islandSpawns.Length, excludeThis);
                Instantiate(coinObject, islandSpawns[nextCoinIndex] + coinOffset, coinObject.transform.rotation);
                numOfCoins++;
                */
            }

            // spawn coins based on island location, based on rates
            yield return new WaitForSeconds(coinRespawnCooldown);
        }
    }

    /* TODO: MOVE
    IEnumerator spawnProjectiles()
    {
        while (true)
        {
            if (gameReadyToStart)
            {
                // pick one at random
                spawnLocations[Random.Range(0, spawnLocations.Count)].spawnRandomly();
            }

            // spawn projectile based on island location, based on rates
            yield return new WaitForSeconds(projectileRespawnCooldown);
        }
    }
    */

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

        // Game is ready to start
        gameReadyToStart = true;
    }

    public void gainCoin(int scoreValue)
    {
        score += scoreValue;
        scoreText.text = score.ToString();
        numOfCoins--;
        
        Debug.Log("Coin Gained. CurrentScore[" + score + "], coinNum["  + numOfCoins + "]");
    }
    
    public void gameOverNow()
    {
        // display game over text
        gameOverProperty = true;
        gameOverText.enabled = true;

        // TODO: check to see if the game is ready to be restarted, delay needed for each restart
    }
}
