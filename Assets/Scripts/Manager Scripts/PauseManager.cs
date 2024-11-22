using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private GameManager gameManager;

    // Reference to the Pause Screen UI
    public GameObject pauseScreenUI; 
    private bool isPaused = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        // Ensure the pause screen is initially hidden
        pauseScreenUI.SetActive(false); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Do not allow pausing when the game is over
            if (gameManager.currentState == GameManager.GameState.GameOver)
            {
                
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
        // Show the pause screen
        pauseScreenUI.SetActive(true); 
        Time.timeScale = 0;
        gameManager.PauseGame();
    }

    void ResumeGame()
    {
        isPaused = false;
        // Hide the pause screen
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
