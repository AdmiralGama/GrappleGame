using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    /// <summary>
    /// Displays the screen
    /// </summary>
    public void Setup()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Reloads the level
    /// </summary>
    public void Retry()
    {
        this.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Quits to main menu
    /// </summary>
    public void Quit()
    {
        this.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("MainMenu");
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
    }
}
