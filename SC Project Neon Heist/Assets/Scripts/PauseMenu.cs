using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject MenuPausa;
    private bool isPaused = false;

    void Update()
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
        if (SceneManager.GetActiveScene().name != "Sandbox")
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Sandbox");
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
        Application.Quit();
    }
}
