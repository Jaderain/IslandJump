using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public int cameraSpeed;

    private GameController gc;
    private Camera cam;
    private Vector3 originalPosition;

    // Use this for initialization
    void Start () {
        // get game object
        gc = Tools.gc;
        cam = GameObject.FindWithTag("Camera").GetComponent<Camera>();
        originalPosition = cam.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		// move slowly back to original position
        // faster at first, become slower at the end
        if (cam.transform.position != originalPosition)
        {
            // get the difference
            // do Z first
            var diff = originalPosition.z - cam.transform.position.z;
            diff /= cameraSpeed;

            // if diffierence is too small, just snap
            if (Math.Abs(diff) <= 0.0001)
            {
                cam.transform.position = originalPosition;
            }
            else
            {
                cam.transform.position += new Vector3(0, 0, diff);
            }
        }
	}

    public void cameraMovementDelayZ(Vector3 movement)
    {
        // do it only Z axis
        // camera should move the opposite way, then recover slowly
        cam.transform.position += new Vector3 (0, 0, movement.z);
    }
}
