using System;
using System.Collections;
using Global;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isGamePaused = false;

    private float cooldownDuration = 0.1f;
    public bool onCooldown = false;

    
    /// <summary>
    /// Called when a player wants to pause the game. Will pause or resume it depending on current state
    /// </summary>
    public void TogglePause()
    {
        if (!onCooldown)
        {
            StartCoroutine(CooldownCoroutine(cooldownDuration));
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    /// <summary>
    /// Pause the game
    /// </summary>
    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    /// <summary>
    /// Resume the game
    /// </summary>
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
        GlobalData.RestartGame();
        StartGame();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    /// <summary>
    /// Close Game
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator CooldownCoroutine(float delay)
    {
        onCooldown = true;
        yield return new WaitForSecondsRealtime(delay);
        onCooldown = false;
    }
}