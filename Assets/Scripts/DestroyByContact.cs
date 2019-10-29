using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;

    private GameController gc;

    private void Start()
    {
        // get game controller
        gc = Tools.gc;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (explosion != null) Instantiate(explosion, transform.position, transform.rotation);
            if (playerExplosion != null) Instantiate(playerExplosion, other.transform.position, other.transform.rotation);

            // game over!
            gc.gameOverNow();

            Destroy(gameObject);
            Destroy(other.gameObject); // dstory player!

            Debug.Log("Current Player is destroyed");
        }
    }
}
