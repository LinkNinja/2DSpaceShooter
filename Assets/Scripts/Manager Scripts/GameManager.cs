using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    // Singleton
    public static GameManager Instance; 

    public enum GameState { Playing, Paused, Dialogue, GameOver }
    public GameState currentState;
    public int playerLives = 3;
    public int playerScore = 0;

    public TMP_Text livesText; 
    public TMP_Text scoreText; 
    public GameObject gameOverScreenUI;

    void Awake()
    {
        // Implement singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1;

        // Update the Lives UI at the start
        UpdateLivesUI();
        // Update the score UI at the start
        UpdateScoreUI(); 
        // Ensure the Game Over screen is initially hidden
        gameOverScreenUI.SetActive(false); 
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
        playerLives = 3;
        playerScore = 0;
        UpdateLivesUI();
        UpdateScoreUI();
    }

    public void StartDialogue()
    {
        currentState = GameState.Dialogue;
        Time.timeScale = 0;
    }

    public void EndDialogue()
    {
        currentState = GameState.Playing;
        // Resume the game
        Time.timeScale = 1; 
    }

    void HandleGameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0;
        // Show the Game Over screen
        gameOverScreenUI.SetActive(true); 
    }

    public void LoseLife()
    {
        playerLives--;
        UpdateLivesUI();
    }

    public void AddScore(int points)
    {
        playerScore += points;
        // Update the score UI
        UpdateScoreUI(); 
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
        SceneManager.LoadScene("TitleScreen");
    }
}
