using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public enum GameState { Playing, Paused, Dialogue, GameOver }
    public GameState currentState;
    public int playerLives = 3;
    public int playerScore = 0;

    public TMP_Text livesText;
    public TMP_Text scoreText;
    public GameObject gameOverScreenUI;

    void Awake()
    {
        // No singleton implementation for now
        DontDestroyOnLoad(gameObject);
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
        // Ensure the new instance handles everything from scratch
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartCoroutine(ResetGameAfterRestart());
    }

    private IEnumerator ResetGameAfterRestart()
    {
        yield return new WaitForEndOfFrame(); // Wait for the scene to reload
        currentState = GameState.Playing;
        Time.timeScale = 1;
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
        Time.timeScale = 1;
    }

    void HandleGameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0;
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
