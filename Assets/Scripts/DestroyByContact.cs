using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;

    private GameController gameController;

    private void Start()
    {
        GameObject gameCtonrollerObject = GameObject.FindWithTag("GameController");
        if (gameCtonrollerObject != null)
        {
            gameController = gameCtonrollerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("gameController is missing!");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (explosion != null) Instantiate(explosion, transform.position, transform.rotation);
            if (playerExplosion != null) Instantiate(playerExplosion, other.transform.position, other.transform.rotation);

            Destroy(gameObject);
            Destroy(other.gameObject); // dstory player!


            // TODO: GAMEOVER
            // TODO: restart
            // TODO: HUD for details
            // gameController.Gameover();

        }
    }
}
