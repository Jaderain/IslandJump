using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class islandScroller : MonoBehaviour {
   
    // game balance setting
    public float scrollspeed;
    
    private GameController gc;
    private CameraController cc;
    
    // Use this for initialization
    void Start()
    {
        // get game object
        gc = Tools.gc;

        // get game controller
        cc = gc.GetComponent<CameraController>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!gc.gameOverProperty)
        {
            moveGameObejects(Vector3.forward * scrollspeed);
            gc.scoreUpdate(true, scrollspeed);
        }
    }

    public void teleport(Vector3 teleportVector)
    {
        if (!gc.gameOverProperty)
        {
            moveGameObejects(teleportVector);
            cc.cameraMovementDelayZ(teleportVector);

            gc.scoreUpdate(true, teleportVector.z);
        }
    }

    private void moveGameObejects(Vector3 moveThisMuch)
    {
        // move all respawn items 
        var respawns = GameObject.FindGameObjectsWithTag("Respawn");
        foreach (GameObject respawn in respawns)
        {
            respawn.transform.position += moveThisMuch;
        }
    }
}
