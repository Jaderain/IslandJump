using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    public float switchDirectionCooldown;
    
    private float nextSwitch = 0.1F;
    private float myTime = 0.0F;
    private PlayerController currentPlayer;
    private GameController gc;

    // Use this for initialization
    void Start () {
        // set current player
        setCurrentPlayer();

        // get game object
        GameObject gameCtonrollerObject = GameObject.FindWithTag("GameController");
        if (gameCtonrollerObject != null)
        {
            gc = gameCtonrollerObject.GetComponent<GameController>();
        }

        if (gc == null)
        {
            Debug.LogError("gameController is missing!");
        }
    }

    // Update is called once per frame
    void Update () {
        // ordinary movement
        myTime = myTime + Time.deltaTime;

        // switch direction on input
        if (Input.GetButtonDown("Fire1") && myTime > nextSwitch)
        {
            nextSwitch = myTime + switchDirectionCooldown;

            // check if game over
            if (gc.gameOverProperty)
            {
                // TODO: restart the game

            }
            else
            {
                // reverse curent projectile
                currentPlayer.reverseRotate();
            }
            
            nextSwitch = nextSwitch - myTime;
            myTime = 0.0F;
        }
    }

    public void setCurrentPlayer()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            currentPlayer = playerObject.GetComponent<PlayerController>();
        }

        if (currentPlayer == null)
        {
            Debug.LogError("Player is missing!");
        }
    }
}
