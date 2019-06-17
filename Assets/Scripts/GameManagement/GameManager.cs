using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject pauseMenu;
    public GameObject gameOverScreen;
    public GameObject player;

    private bool gameIsPaused = false;

    #region Singleton Patter
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

    public Transform GetPlayerTransform()
    {
        return player.transform;
    }
}