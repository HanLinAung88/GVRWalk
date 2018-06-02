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
    public Text restartText;

    private bool restart = false;
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
        restartText.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
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
            restartText.gameObject.SetActive(true);
            StartCoroutine("restartGame");
        }
	}

    /*
     * This method sets the score of the text on the canvas
     */
    private void setScoreText() {
        scoreText.text = "Score: " + scoreCoin.ToString();
    }

    IEnumerator restartGame() {
        yield return new WaitForSeconds(3);
        restart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        restart = false;
    }

}
