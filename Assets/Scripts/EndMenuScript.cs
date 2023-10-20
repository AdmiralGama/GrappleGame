using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class EndMenuScript : MonoBehaviour
{
    /// <summary>
    /// Returns to the main menu
    /// </summary>
    public void Quit()
    {
        this.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("MainMenu");
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Quit();
        }
    }
}
