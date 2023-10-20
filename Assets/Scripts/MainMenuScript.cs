using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenuScript : MonoBehaviour
{
    string firstLevel = "Tutorial";
    string saveFile = "save";

    /// <summary>
    /// Loads the first level
    /// </summary>
    public void StartButton()
    {
        this.GetComponent<AudioSource>().Play();

        string scene = firstLevel;

        // If there is no save file, make a new one
        if (!File.Exists(Application.persistentDataPath + "/" + saveFile))
        {
            using (StreamWriter writer = new(File.OpenWrite(Application.persistentDataPath + "/" + saveFile)))
            {
                writer.WriteLine(firstLevel);
            }
        }

        // Loads the last level in the save file
        using (StreamReader reader = new(Application.persistentDataPath + "/" + saveFile))
        {
            string saveText = reader.ReadLine();

            if (saveText != null)
            {
                scene = saveText.Split(',')[saveText.Split(',').Length - 1];
            }
        }

        SceneManager.LoadScene(scene);
    }

    /// <summary>
    /// Deletes the save file
    /// </summary>
    public void Erase()
    {
        this.GetComponent<AudioSource>().Play();
        File.Delete(Application.persistentDataPath + "/" + saveFile);
    }

    /// <summary>
    /// Closes the game
    /// </summary>
    public void Quit()
    {
        this.GetComponent<AudioSource>().Play();
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            StartButton();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            Quit();
        }

        if (Input.GetButtonDown("Lock"))
        {
            Erase();
        }
    }
}
