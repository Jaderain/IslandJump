using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class islandScroller : MonoBehaviour {
    
    // island Setup
    public GameObject islandObject;
    public Vector3[] islandSpawnsDefaultLocation;

    // game balance setting
    public float scrollspeed;

    // private matters
    private islandLinkdList head;
    private islandLinkdList tail;

    class islandLinkdList
    {
        public islandLinkdList prev;
        public islandLinkdList next;
        public int id; // TODO?
        public GameObject island;

        public islandLinkdList()
        {
            prev = null;
            next = null;
        }

        public islandLinkdList(GameObject inputObject, Vector3 inputLoc, 
            islandLinkdList inputPrev, islandLinkdList inputNext)
        {
            island = Instantiate(inputObject,
                inputLoc,
                Quaternion.Euler(new Vector3(0, 45, 0)));
            prev = inputPrev;
            next = inputNext;
        }
    }
    // Use this for initialization
    void Start()
    {
        // spawn islandObjects of given
        foreach (Vector3 item in islandSpawnsDefaultLocation)
        {
            if (head == null)
            {
                head = new islandLinkdList();
                head.island = Instantiate(islandObject, item, Quaternion.Euler(new Vector3(0, 45, 0)));
                tail = head;
            }
            else
            {
                addIslandToGraph(false, item);
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        // move other stuff
        var respawns = GameObject.FindGameObjectsWithTag("Respawn");
        foreach (GameObject respawn in respawns)
        {
            respawn.transform.position += Vector3.forward * scrollspeed;
        }
        
        // move the islands
        var traverse = head;
        while (traverse != null)
        {
            // see if anyone of them is destroyed
            if (traverse.island == null)
            {
                // make sure prev and next one is connected
                if (traverse.next != null)
                {
                    traverse.next.prev = traverse.prev;
                }

                if (traverse.prev != null)
                {
                    traverse.prev.next = traverse.next;
                }

                // move tail and head with it
                if (traverse == head)
                {
                    head = traverse.prev;
                }
                else if (traverse == tail)
                {
                    tail = traverse.next;
                }
            }
            else
            {
                traverse.island.transform.position += Vector3.forward * scrollspeed;
            }

            traverse = traverse.prev;
        }
        
        // see if last one is below 2nd to last element or the other way
        // then respawn
        if (head.island.transform.position.z
            <= islandSpawnsDefaultLocation[1].z)
        {
            addIslandToGraph(true, islandSpawnsDefaultLocation[0]);
        }

        if (tail.island.transform.position.z
            >= islandSpawnsDefaultLocation[islandSpawnsDefaultLocation.Length - 2].z)
        {
            addIslandToGraph(false, islandSpawnsDefaultLocation[islandSpawnsDefaultLocation.Length - 1]);
        }
        
        /*
        // DEBUG: for debug purposes
        traverse = head;
        while (traverse != null)
        {
            if (traverse == head)
            {
                traverse.island.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
            }
            else if (traverse == tail)
            {
                traverse.island.GetComponent<Renderer>().material.color = new Color(0, 0, 255);
            }
            else
            {
                traverse.island.GetComponent<Renderer>().material.color = new Color(170, 255, 0);
            }
            traverse = traverse.prev;
        }
        */
    }

    private void addIslandToGraph(bool trueIfHead, Vector3 inputLoc)
    {
        if (trueIfHead)
        {
            // -> x -> y -> z -> head -> new -> null
            head.next = new islandLinkdList(islandObject, inputLoc, head, null);
            head = head.next;
        }
        else
        {
            // null -> new -> tail -> x -> y -> z -> 
            tail.prev = new islandLinkdList(islandObject, inputLoc, null, tail);
            tail = tail.prev;
        }
    }
}
