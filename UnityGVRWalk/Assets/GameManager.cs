using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    private int scoreCoin = 0;
    public Text scoreText; //the UI for the score
    public Text restartText;
    public Text highScoreText;
    public Text completeLvlText;
    private bool completeLevel;

    /**
     * Initializes the UI by setting the restart text/variables to false
     */
    void Start()
    {
        restartText.gameObject.SetActive(false);
        completeLvlText.gameObject.SetActive(false);
        completeLevel = false;
    }

    /**
     * Update is called once per frame to set the text of the score
     */
    void Update()
    {
        setScoreText();
    }

    /**
     * This method is used to increment the score when a coin is collected
     */
    public void incrementScore()
    {
        scoreCoin++;
        Debug.Log(scoreCoin);
    }

    /**
     * This method is called when the player reaches the destination and
     * the level is completed. Restarts the game when level is completed
     */
    public void completeLvl()
    {
        completeLevel = true;
        completeLvlText.gameObject.SetActive(true);
        resetGame();
    }

    /**
     * This method restarts the game and shows the restart text
     */
    public void resetGame()
    {
        if (!completeLevel)
        {
            restartText.gameObject.SetActive(true);
        }
        StartCoroutine("restartGame");
    }
    /**
    * This method sets the score of the text on the canvas
    */
    private void setScoreText()
    {
        scoreText.text = "Score: " + scoreCoin.ToString();
        int prevHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (scoreCoin > prevHighScore)
        {
            PlayerPrefs.SetInt("HighScore", scoreCoin);
        }
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    /**
     * This coroutine method restarts the game after waiting for 3 seconds
     */
    IEnumerator restartGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
