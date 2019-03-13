using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGain : MonoBehaviour {

    public int coinValue;

    private GameController gc;

    // Use this for initialization
    void Start ()
    {
        GameObject gameCtonrollerObject = GameObject.FindWithTag("GameController");
        if (gameCtonrollerObject != null)
        {
            gc = gameCtonrollerObject.GetComponent<GameController>();
        }
        if (gc == null)
        {
            Debug.Log("gameController is missing!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // gain coin
        gc.GainCoin(coinValue);
        Destroy(gameObject);
    }
}
