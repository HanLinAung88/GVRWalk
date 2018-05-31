using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class autoWalk : MonoBehaviour {
	public float speed = 3.0F;

	public bool moveForward;

	private CharacterController controller;

	//private GvrEditorEmulator gvrEditor;

	private Transform vrHead;
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();

		//gvrEditor = transform.GetChild (0).GetComponent<GvrEditorEmulator> ();

		vrHead = Camera.main.transform;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			moveForward = !moveForward;
		}

		if (moveForward) {
			//find the forward direction
			Vector3 forward = vrHead.TransformDirection (Vector3.forward);
			//use controller to move forward
			controller.SimpleMove (forward * speed);
		}
	}
}
