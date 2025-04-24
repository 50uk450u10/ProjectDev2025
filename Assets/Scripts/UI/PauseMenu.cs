using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    private bool quitButtonPressed = false;
    private bool settingsButtonPressed = false;
    public GameObject pauseMenuUI;
    public GameObject confirmQuitPanel;
    public GameObject buttonPanel;
    public GameObject settingsPanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused && !settingsButtonPressed)
            {
                Resume();
            }
            else if(settingsButtonPressed)
            {
                Settings();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        if (quitButtonPressed)
        {
            confirmQuitPanel.SetActive(false);
            buttonPanel.SetActive(true);
            quitButtonPressed = false;
        }
        else
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1.0f;
            GameIsPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            quitButtonPressed = false;
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        buttonPanel.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Quit()
    {
        if (!quitButtonPressed)
        {
            quitButtonPressed = true;
            confirmQuitPanel.SetActive(true);
            buttonPanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 1.0f;
            quitButtonPressed = false;
            SceneManager.LoadScene(0);      //sends player back to main menu
        }
    }

    public void Settings()
    {
        if (settingsButtonPressed)
        {
            settingsButtonPressed = false;
            settingsPanel.SetActive(false);
            buttonPanel.SetActive(true);
        }
        else
        { 
            settingsButtonPressed = true;
            settingsPanel.SetActive(true);
            buttonPanel.SetActive(false);
        }
    }

}
