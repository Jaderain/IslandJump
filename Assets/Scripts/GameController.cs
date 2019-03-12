using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject islandObject;
    public Vector3[] islandSpawns;

    // Use this for initialization
    void Start() {
        initPlatforms();
    }

    // Update is called once per frame
    void Update() {

    }

    void initPlatforms()
    {
        foreach (Vector3 item in islandSpawns)
        {
            Instantiate(islandObject, item, Quaternion.Euler(new Vector3(0, 45, 0)));
        }
    }
}
