using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    // private values for running game
    private GameController gc;

    // Use this for initialization
    void Start () {
        // get game controller
        gc = Tools.gc;

        // first destination - first on the list
        // TODO: nextDestination = gc.islandSpawns[0];
    }
	
	// Update is called once per frame
	void Update ()
    {

    }
}
