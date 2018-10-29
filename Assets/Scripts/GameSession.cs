using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {

    [SerializeField] public int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] public float RespawnTime = 2f;

    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {

            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
	}

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            StartCoroutine(ResetGameSession());
        }
    }

    private void TakeLife()
    {
        playerLives--;
        //Invoke("ResetLevel", RespawnTime);
        StartCoroutine(ResetLevel());
        livesText.text = playerLives.ToString();
    }


    IEnumerator ResetLevel()
    {
        yield return new WaitForSecondsRealtime(RespawnTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator ResetGameSession()
    {
        yield return new WaitForSecondsRealtime(RespawnTime);
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
