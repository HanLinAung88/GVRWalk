using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//TODO: shooting the sound, and the final destination scene, editor keystroke
[RequireComponent(typeof(CharacterController))]
public class autoWalk : MonoBehaviour {
    public float speed = 1.5F;
    private float gravity = 14.0F;
    public float verticalVelocity;
    private float jumpForce = 10.0f;

    private bool moveForward;
 
    //The character controller controlling the movement
	private CharacterController controller;

	private GvrEditorEmulator gvrEditor;

	private Transform vrHead;
    private bool isInAir;
    private bool moveForwardInAir;
    private GameManager gameManager;  //the gameManager(controlling the state)


	/**
	 * Initializes the controller, and necessary variables for the movement
	 */
	void Start () 
    {
		controller = GetComponent<CharacterController> ();
		gvrEditor = transform.GetChild (0).GetComponent<GvrEditorEmulator> ();
		vrHead = Camera.main.transform;
       
        isInAir = false;
        moveForwardInAir = false;
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
      }

	/**
	 * Update is called once per frame to control movement of the player
	 */
	void Update () 
    {
        if(!Application.isEditor && Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.W)) 
        {
            //controls movement when player is in air to not abruptly stop
            if(moveForward && isInAir) {
                moveForwardInAir = true;
            }
            moveForward = !moveForward;
        }    

        /* For debugging purposes in the editor */
        if (Application.isEditor)
        {
            float h = 4.0F * Input.GetAxis("Mouse X");
            transform.Rotate(0, h, 0);
        }

        /* Call function for Jump action */
        velocityJump();
        Vector3 moveVector = new Vector3(0, verticalVelocity, 0);

        /* Ensures player does not abruptly stop while jumping in the air */
        if(moveForward || moveForwardInAir) {
            moveVector = vrHead.forward * speed;
            moveVector.y = verticalVelocity;
            if(controller.isGrounded)
            {
                moveForwardInAir = false;
            }    
        }
     
        //moveVector.x = Input.GetAxis("Horizontal") * 5.0f;
        //moveVector.z = Input.GetAxis("Vertical") * 5.0f;
        controller.Move(moveVector * Time.deltaTime);
    }


    /**
     * This method is called to handle the jumping movement of the player
     */
    private void velocityJump()
    {
        if (controller.isGrounded)
        {
            isInAir = false;
            verticalVelocity = -gravity * Time.deltaTime;
            //changes vertical velocity to an upward motion if jump is requested
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
            {
                verticalVelocity = jumpForce;
                isInAir = true;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
    }

    /*
     * This method checks if it collides with another object and is used to 
     * check for collision with game coins
     */
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
        if(hit.gameObject.CompareTag("Ground")) 
        {
            gameManager.resetGame();
        } 
        if(hit.gameObject.CompareTag("Destination"))
        {
            gameManager.completeLvl();
        }

        if(hit.gameObject.CompareTag("MovingPlatform1"))
        {
            this.transform.parent = hit.transform;
        }
        else
        {
            this.transform.parent = null;
        }
    }

}

