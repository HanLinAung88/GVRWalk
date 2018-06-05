using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRestart : MonoBehaviour {

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hello");
        if (other.gameObject.CompareTag("MainController"))
        {
            gameManager.resetGame();
        }
    }
}
