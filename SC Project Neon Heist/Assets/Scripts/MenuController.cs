using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject MenuPausa;
    private bool isPaused = false;

    void Update()
    {
        string scene = SceneManager.GetActiveScene().name;
        if ((scene == "Game" || scene == "SandBox") && MenuPausa != null)
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        MenuPausa.SetActive(true);
        isPaused = true;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        MenuPausa.SetActive(false);
        isPaused = false;
    }
    public void loadSandbox()
    {
        if (SceneManager.GetActiveScene().name != "SandBox")
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("SandBox");
        }
        else
        {
            Resume();
        }
    }

    public void LoadMainGame()
    {
        if (SceneManager.GetActiveScene().name != "Game")
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Game");
        }
        else
        {
            Resume();
        }
    }
    public void ExitGame()
    {
        Debug.Log("salio del juego");
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
