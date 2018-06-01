using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(CharacterController))]
public class autoWalk : MonoBehaviour {
	public float speed = 1.5F;
    private float gravity = 14.0F;
    public float verticalVelocity;
    private float jumpF = 10.0f;

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
        if(this.transform.position.y < -50) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(controller.isGrounded) {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetButtonDown("Fire1"))
            {
                verticalVelocity = jumpF;
                //moveUp = !moveUp;
                //moveForward = !moveForward;
            }

        } else {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        Vector3 move = Camera.main.transform.forward * speed;

        //move.x = Input.GetAxis("Horizontal") * 5.0f;
        move.y = verticalVelocity;
        //move.z = Input.GetAxis("Vertical") * 0.0f;
        controller.Move(move * Time.deltaTime);
            
       
	}
}
