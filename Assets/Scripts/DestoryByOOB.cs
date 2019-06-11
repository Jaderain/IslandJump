using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryByOOB : MonoBehaviour {

    void OnTriggerExit(Collider other)
    {
        // Destroy everything that leaves the trigger
        Destroy(other.gameObject);
    }
}
