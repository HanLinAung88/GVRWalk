using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(CharacterController))]
public class autoWalk : MonoBehaviour {
	public float speed = 1.5F;
    private float gravity = 14.0F;
    public float verticalVelocity;
    private float jumpF = 10.0f;
    private int scoreCoin = 0; //the score on the number of coins picked

	public bool moveForward = true;
    public bool moveUp;
    public Text scoreText; //the UI for the score

    //The character controller controlling the movement
	private CharacterController controller;

	private GvrEditorEmulator gvrEditor;

	private Transform vrHead;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		gvrEditor = transform.GetChild (0).GetComponent<GvrEditorEmulator> ();
		vrHead = Camera.main.transform;

        setScoreText();
	}

	// Update is called once per frame
	void Update () {
        //if(this.transform.position.y < -50) {
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //}

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

        Vector3 move = vrHead.forward * speed;
        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime);
            
       
	}

    /*
     * This method checks if it collides with another object and is used to 
     * check for collision with game coins
     */
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
        if(hit.gameObject.CompareTag("Coin")) {
            Destroy(hit.gameObject);
            scoreCoin++;
            setScoreText();
        } else if(hit.gameObject.CompareTag("Ground")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
	}

    /*
     * This method sets the score of the text on the canvas
     */
    private void setScoreText() {
        scoreText.text = "Score: " + scoreCoin.ToString();
    }

    private void restartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        scoreCoin = 0;
    }

}
