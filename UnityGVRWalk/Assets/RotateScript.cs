using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script handles the rotation of the coin
public class RotateScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame to rotate the coin
	void Update () {
        transform.Rotate(new Vector3(0,1,0) * 3, Space.World);
	}
}
