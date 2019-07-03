using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject pauseMenu;
    public GameObject gameOverScreen;
    public GameObject winScreen;

    private GameObject player;
    private bool gameIsPaused = false;
    private bool gameIsOver = false;

    #region Singleton Pattern
    //*****************
    // Singleton pattern
    // https://gamedev.stackexchange.com/a/116010/123894
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        // Singleton Enforcement Code
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Game Actions
    public void Pause()
    {
        gameIsPaused = true;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        gameIsPaused = false;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public bool GameIsPaused()
    {
        return gameIsPaused;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        gameIsOver = true;
        pauseButton.SetActive(false);
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }    

    public void Restart()
    {
        Debug.Log("Restart game");
        LevelChanger.Instance.FadetoScene(sceneIndex: 1);
        Time.timeScale = 1f;
        gameIsOver = false;
    }

    public void Quit()
    {
        Debug.Log("Quit game");        
        LevelChanger.Instance.FadetoScene(sceneIndex: 0);
        Time.timeScale = 1f;
    }

    public void LevelComplete()
    {
        Debug.Log("Level Complete!");
        gameIsOver = true;
        pauseButton.SetActive(false);
        winScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public Transform GetPlayerTransform()
    {
        return player.transform;
    }
}