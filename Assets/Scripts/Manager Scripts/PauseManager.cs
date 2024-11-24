using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject pauseScreenUI; // Reference to the Pause Screen UI
    private bool isPaused = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        pauseScreenUI.SetActive(false); // Ensure the pause screen is initially hidden
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameManager.currentState == GameManager.GameState.GameOver)
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
        pauseScreenUI.SetActive(true); // Show the pause screen
        Time.timeScale = 0;
        gameManager.PauseGame();
    }

    void ResumeGame()
    {
        isPaused = false;
        pauseScreenUI.SetActive(false); // Hide the pause screen
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
