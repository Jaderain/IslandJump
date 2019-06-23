using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tools {

    public static GameController gc { get; private set; }

    static Tools()
    {
        // get game controller
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
}
