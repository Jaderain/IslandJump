using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    
    // player prefab
    public GameObject playerPrefab;
    
    // accessible properties
    public bool gameReadyToStart { get; private set; }
    // public bool gameMenuProperty { get; private set; } TODO: Menu option
    public bool gameOverProperty { get; private set; }
    public PlayerController currentPlayerContProperty { get; private set; }

    // simple game properties
    public float delayBeforeGameStarts;

    // in game scores and stuff
    private float score;

    // in game other UI
    private Text scoreText;
    private Text gameOverText;
   
    void Start()
    {
        // Use this for initialization
        currentPlayerContProperty = null;
        score = 0;
        gameReadyToStart = false;
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
                scoreUpdate(false, 0);
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

        // ready to start;
        gameReadyToStart = true;
        playerStart();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void initPlatforms()
    {
        // TODO: use this section to initialize enviornment
    }
    
    /* public method to be called from other scripts for updates */
    public void playerStart()
    {
        if (gameReadyToStart)
        {
            // Spawn a player 
            if (currentPlayerContProperty == null)
            {
                // reset values
                gameOverProperty = false;
                gameOverText.enabled = false;
                scoreUpdate(false, 0);

                // spawn a plyaer
                var player = Instantiate(playerPrefab, new Vector3(0, 1, 3), Quaternion.Euler(new Vector3(0, 45, 0)));
                currentPlayerContProperty = player.GetComponent<PlayerController>();
            }

            gameReadyToStart = false;
        }
    }
    
    public void gameOverNow()
    {
        gameReadyToStart = false;

        // display game over text
        gameOverProperty = true;
        gameOverText.enabled = true;

        // wait a bit before game restarts
        StartCoroutine(gameReadyAfterADelay());
    }

    public void scoreUpdate(bool tureIfAdd_falseIfSet, float inputScore)
    {
        if (!gameOverProperty)
        {
            if (tureIfAdd_falseIfSet) score += inputScore;
            else score = inputScore;

            // integer score only
            scoreText.text = Math.Abs(score).ToString("0.00"); // always positive score
        }
    }

    IEnumerator gameReadyAfterADelay()
    {
        yield return new WaitForSeconds(delayBeforeGameStarts);

        // Code to execute after the delay
        gameReadyToStart = true;
    }
}
