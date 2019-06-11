using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    // get inputs for this method parameter
    public float speed;
    public Vector3 offset;
    public bool rotateClockwise;
    public float switchDirectionCooldown;

    // private values for running game
    private GameController gc;
    private Vector3 nextDestination;
    private float nextSwitch = 0.1F;
    private float myTime = 0.0F;

    // Use this for initialization
    void Start () {
        GameObject gameCtonrollerObject = GameObject.FindWithTag("GameController");
        if (gameCtonrollerObject != null)
        {
            gc = gameCtonrollerObject.GetComponent<GameController>();
        }
        if (gc == null)
        {
            Debug.Log("gameController is missing!");
        }

        // first destination - first on the list
        nextDestination = gc.islandSpawns[0];
    }
	
	// Update is called once per frame
	void Update ()
    {
        myTime = myTime + Time.deltaTime;

        // switch direction on input
        if (Input.GetButtonDown("Fire1") && myTime > nextSwitch)
        {
            nextSwitch = myTime + switchDirectionCooldown;

            rotateClockwise = !rotateClockwise;
            nextLocation(true);

            nextSwitch = nextSwitch - myTime;
            myTime = 0.0F;
        }
    }

    // Called based on framerates;
    private void FixedUpdate()
    {
        // update next destination
        nextLocation(false);

        // Move our position a step closer to the target.
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, nextDestination + offset, step);
    }

    private void nextLocation(bool immediateChange)
    {
        // Check if the position of the cube and sphere are approximately equal.
        if (immediateChange || Vector3.Distance(transform.position, nextDestination + offset) < 0.001f)
        {
            for (int i = 0; i < gc.islandSpawns.Length; i++)
            {
                if (gc.islandSpawns[i] == nextDestination)
                {
                    int nextDestinationIndex;

                    if (rotateClockwise)
                    {
                        if (i == gc.islandSpawns.Length - 1) nextDestinationIndex = 0;
                        else nextDestinationIndex = i + 1;
                    }
                    else
                    {
                        if (i == 0) nextDestinationIndex = gc.islandSpawns.Length - 1;
                        else nextDestinationIndex = i - 1;
                    }

                    // reached destination, get the next one in the list
                    nextDestination = gc.islandSpawns[nextDestinationIndex];
                    break;
                }
            }
        }
    }
}
