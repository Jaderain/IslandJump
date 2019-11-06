using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    public float inputCooldown;
    public Vector3 teleportDistance;

    private float nextSwitch = 0.1F;
    private float myTime = 0.0F;
    private GameController gc;
    private islandScroller isc;

    // Use this for initialization
    void Start () {
        // get game object
        gc = Tools.gc;

        // get game controller
        isc = gc.GetComponent<islandScroller>();
    }

    // Update is called once per frame
    void Update () {
        // ordinary movement
        myTime = myTime + Time.deltaTime;

        // switch direction on input
        if (Input.GetButtonDown("Fire1") && myTime > nextSwitch)
        {
            nextSwitch = myTime + inputCooldown;

            // check if game over
            if (gc.gameOverProperty)
            {
                gc.playerStart();
            }
            else
            {
                isc.teleport(teleportDistance); 
            }
            
            nextSwitch = nextSwitch - myTime;
            myTime = 0.0F;
        }
    }
}
