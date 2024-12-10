using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject pauseScreenUI; 
    private bool isPaused = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        pauseScreenUI.SetActive(false); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameManager.currentState == GameManager.GameState.GameOver || gameManager.currentState == GameManager.GameState.Dialogue)
            {
                // Do not allow pausing when the game is over
                return;
            }

            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {

        isPaused = true;
        pauseScreenUI.SetActive(true); 
        Time.timeScale = 0;
        gameManager.PauseGame();
    }

    void ResumeGame()
    {
        isPaused = false;      
        pauseScreenUI.SetActive(false); 
        Time.timeScale = 1;
        gameManager.ResumeGame();
    }

    public void QuitGame()
    {
        gameManager.QuitGame();
    }

    public void ReturnToMainMenu()
    {
        gameManager.ReturnToMainMenu();
    }
}
