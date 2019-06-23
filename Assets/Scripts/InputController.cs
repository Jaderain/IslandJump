using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    public float switchDirectionCooldown;

    private float nextSwitch = 0.1F;
    private float myTime = 0.0F;
    private GameController gc;

    // Use this for initialization
    void Start () {
        // get game object
        gc = Tools.gc;
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
                gc.playerStart();
            }
            else
            {
                // reverse curent player, if exists
                if (gc.currentPlayerContProperty != null)
                {
                    gc.currentPlayerContProperty.reverseRotate();
                }
            }
            
            nextSwitch = nextSwitch - myTime;
            myTime = 0.0F;
        }
    }
}
