using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour {

    [System.Serializable]
    public class spawnPoint
    {
        public Vector3 spawnMin;
        public Vector3 spawnMax;
        public Quaternion rotation;
    }

    public float spawnRate;
    public GameObject projectileObject;
    public spawnPoint[] inputSpawnPoint;

    // Use this for initialization
    void Start () {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            foreach (var item in inputSpawnPoint)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(item.spawnMin.x, item.spawnMax.x),
                    Random.Range(item.spawnMin.y, item.spawnMax.y),
                    Random.Range(item.spawnMin.z, item.spawnMax.z));

                Instantiate(projectileObject, spawnPosition, item.rotation);
            }

            yield return new WaitForSeconds(spawnRate);
        }
    }
}
