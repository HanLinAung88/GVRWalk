using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class autoWalk : MonoBehaviour {
	public float speed = 1.5F;
    private float gravity = 14.0F;
    public float verticalVelocity;
    private float jumpForce = 10.0f;

	public bool moveForward = true;
    public bool moveUp;

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

        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        Vector3 moveVector = new Vector3(0, verticalVelocity, 0);
        moveVector.x = Input.GetAxis("Horizontal") * 5.0f;
        moveVector.z = Input.GetAxis("Vertical") * 5.0f;
        controller.Move(moveVector * Time.deltaTime);
    }
            
}
