using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject pauseMenu;
    public GameObject gameOverScreen;

    private bool gameIsPaused = false;

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
}
