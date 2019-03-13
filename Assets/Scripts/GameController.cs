using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    // island Setup
    public GameObject islandObject;
    public Vector3[] islandSpawns;

    // coin spawn setup
    public GameObject coinObject;
    public int maxCoinsOnTheField;
    public Vector3 coinOffset;
    public float coinInitialCooldown;
    public float coinRespawnCooldown;
    
    private int score;
    private int coinsOnField;

    // Use this for initialization
    void Start() {
        score = 0;
        coinsOnField = 0;

        // init platforms
        initPlatforms();

        // spawn coins
        StartCoroutine(spawnCoins());

        Debug.Log("Game Started, Init done");
    }

    // Update is called once per frame
    void Update()
    {
        // spawn coin every once in awhile on the platform

    }

    void initPlatforms()
    {
        foreach (Vector3 item in islandSpawns)
        {
            Instantiate(islandObject, item, Quaternion.Euler(new Vector3(0, 45, 0)));
        }
    }
    
    IEnumerator spawnCoins()
    {
        yield return new WaitForSeconds(coinInitialCooldown);
        while (true)
        {
            if (coinsOnField < maxCoinsOnTheField)
            {
                int respawnPointIndex = Random.Range(0, islandSpawns.Length - 1);
                
                Instantiate(coinObject, islandSpawns[respawnPointIndex] + coinOffset, coinObject.transform.rotation);
                coinsOnField++;

                yield return new WaitForSeconds(coinRespawnCooldown);
            }
        }
    }

    public void GainCoin(int scoreValue)
    {
        score += scoreValue;
        coinsOnField--;

        Debug.Log("Coin Gained. CurrentScore[" + score
            + "], coinsOnField[" + coinsOnField + "]");
    }
}
