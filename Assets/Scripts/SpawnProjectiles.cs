using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour {
    
    public float beamLength;
    public GameObject projectileObject;
    public float projectileRespawnCooldown;

    // Use this for initialization
    void Start () {
        Debug.Log("a new SpawnBlockHere" + transform.position);
        StartCoroutine(spawnProjectiles());
    }
	
    IEnumerator spawnProjectiles()
    {
        // TODO: Balance Issue - spawning on the line of movement near-impossible to dodge
        while (true)
        {
            // pick one of the range in random
            float beamLengthHalf = beamLength / 2.0f;
            Vector3 spawnPosition = new Vector3(Random.Range((transform.position.x - beamLengthHalf), (transform.position.x + beamLengthHalf)), transform.position.y, transform.position.z);

            Debug.Log("Projectile on Vector3[" + spawnPosition + "]");

            // create the projectile object
            Instantiate(projectileObject, spawnPosition, transform.rotation);

            // spawn projectile based on island location, based on rates
            yield return new WaitForSeconds(projectileRespawnCooldown);
        }
    }
}
