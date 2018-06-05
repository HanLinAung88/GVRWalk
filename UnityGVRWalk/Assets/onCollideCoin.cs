using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onCollideCoin : MonoBehaviour {

    // Use this for initialization
    public AudioClip clip;
    private GameManager gameManager;

    /**
     * Initializes the gameManager object/script
     */
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

    }
    /**
     * This method is called when the controller/player collects the coin
     * The sound is played and the score is incremented 
     */
	private void OnTriggerExit(Collider other)
	{
        if(other.gameObject.CompareTag("MainController"))
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
            gameManager.incrementScore();
            Destroy(gameObject);
        }
	}

}
