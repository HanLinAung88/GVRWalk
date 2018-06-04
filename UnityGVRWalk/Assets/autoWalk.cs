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

    private int scoreCoin = 0; //the score on the number of coins picked

    public bool moveForward = false;
    public bool moveUp;
    public Text scoreText; //the UI for the score
    public Text restartText;
    public Text highScoreText;


    AudioSource aScource;

    //The character controller controlling the movement
	private CharacterController controller;

	private GvrEditorEmulator gvrEditor;

	private Transform vrHead;

	// Use this for initialization
	void Start () 
    {
		controller = GetComponent<CharacterController> ();
		gvrEditor = transform.GetChild (0).GetComponent<GvrEditorEmulator> ();
		vrHead = Camera.main.transform;
        setScoreText();
        restartText.gameObject.SetActive(false);
        aScource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () 
    {
        if(!Application.isEditor && Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.W)) 
        {
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

        if(moveForward) {
            moveVector = vrHead.forward * speed;
            moveVector.y = verticalVelocity;
        }
     
        //moveVector.x = Input.GetAxis("Horizontal") * 5.0f;
        //moveVector.z = Input.GetAxis("Vertical") * 5.0f;
        controller.Move(moveVector * Time.deltaTime);
    }

    private void velocityJump()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
            {
                verticalVelocity = jumpForce;
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
        if(hit.gameObject.CompareTag("Coin")) 
        {
            Destroy(hit.gameObject);
            scoreCoin++;
            aScource.Play();
            setScoreText();
        } 
        else if(hit.gameObject.CompareTag("Ground")) 
        {
            restartText.gameObject.SetActive(true);
            scoreCoin = 0;
            StartCoroutine("restartGame");
        }
	}

    /*
     * This method sets the score of the text on the canvas
     */
    private void setScoreText() {
        scoreText.text = "Score: " + scoreCoin.ToString();
        int prevHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if(scoreCoin > prevHighScore) 
        {
            PlayerPrefs.SetInt("HighScore", scoreCoin);
        }
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    /*  
     * This method restarts the game after waiting for 3 seconds
     */
    IEnumerator restartGame() 
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}

