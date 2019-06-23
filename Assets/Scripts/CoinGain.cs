using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGain : MonoBehaviour {

    public int coinValue;

    private GameController gc;

    // Use this for initialization
    void Start ()
    {
        // gain game controller
        gc = Tools.gc;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // gain coin
            gc.gainCoin(coinValue);
            Destroy(gameObject);
        }
    }
}
