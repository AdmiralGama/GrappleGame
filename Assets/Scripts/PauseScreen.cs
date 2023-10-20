using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    /// <summary>
    /// Opens menu and pauses game
    /// </summary>
    public void Setup()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }

    /// <summary>
    /// Restarts level
    /// </summary>
    public void Retry()
    {
        this.GetComponent<AudioSource>().Play();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Quits to main menu
    /// </summary>
    public void Quit()
    {
        this.GetComponent<AudioSource>().Play();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Closes the menu
    /// </summary>
    public void UnPause()
    {
        this.GetComponent<AudioSource>().Play();
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Retry();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            Quit();
        }

        if (Input.GetButtonDown("Pause"))
        {
            UnPause();
        }
    }
}
