using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GroundRestart : MonoBehaviour {

	// Use this for initialization
    void OnTriggerEnter() {
        SceneManager.LoadScene("InitialScene");
    }
}
