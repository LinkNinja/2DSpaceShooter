using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum GameState { Playing, Paused, GameOver }
    public GameState currentState;
    public int playerLives = 3;
    public int playerScore = 0;

    public TMP_Text livesText; // Reference to the TextMeshPro UI element
    public TMP_Text scoreText; // Reference to the TextMeshPro UI element for score
    public GameObject gameOverScreenUI; // Reference to the Game Over screen UI

    void Start()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1;

        UpdateLivesUI();
        UpdateScoreUI(); // Update the score UI at the start
        gameOverScreenUI.SetActive(false); // Ensure the Game Over screen is initially hidden
    }

    void FixedUpdate()
    {
      
        if (playerLives <= 0)
        {
            currentState = GameState.GameOver;
            HandleGameOver();
        }
    }

    public void PauseGame()
    {
        currentState = GameState.Paused;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        currentState = GameState.Playing;
        playerLives = 3;  // Adjust if needed
        playerScore = 0;  // Adjust if needed
        UpdateLivesUI();
        UpdateScoreUI(); // Reset the score UI
    }

    void HandleGameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0;
        gameOverScreenUI.SetActive(true); // Show the Game Over screen
    }

    public void LoseLife()
    {
        playerLives--;
        UpdateLivesUI();
    }

    public void AddScore(int points)
    {
        playerScore += points;
        UpdateScoreUI(); // Update the score UI when points are added
    }

    void UpdateLivesUI()
    {
        livesText.text = "Lives: " + playerLives;
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + playerScore;
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {

        Debug.Log("Returning to Main Menu");
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScreen"); // Replace "MainMenu" with your actual main menu scene name
    }
}
