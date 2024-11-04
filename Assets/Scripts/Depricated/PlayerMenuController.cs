using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMenuController : MonoBehaviour
{


    public int mainMenuScene;
    public GameObject pauseMenu;
    public bool isPaused;
    //private GameManager gameManager;


    // Start = First Frame
    void Start()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update = Once per frame
    void Update()
    {
        // Checking if the player is pressing the Escape Key
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            // Checking if the game is already paused when hitting the escape Key. This will resume the game. 
            // ELSE After checking if the game is already paused. This will bring up the Pause menu
            if (isPaused)
            {
                ResumeGame();

            }
         
            else
            {
                //run code here if game is not paused 
                isPaused = true;
                pauseMenu.SetActive(true);
                //stoping time in the game and making it equal to 0, basically saying time in the game is moving and 0 times the speed.
                //This can be used to slow down time in the game like bullet time. 
                Time.timeScale = 0f;
            }
        }

    }

    public void ResumeGame()
    {
        //Run code here if game is paused
        isPaused = false;
        pauseMenu.SetActive(false);
        //here we are turning the time back on.
        Time.timeScale = 1f;
    }

    public void ReturnToMain()
    {
        //when going back to main menu from pause screen dont forget to set time back to normal if you stopped time during the pause menu.
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }



}
