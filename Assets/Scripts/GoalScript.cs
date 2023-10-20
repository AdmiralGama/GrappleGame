using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class GoalScript : MonoBehaviour
{
    public string nextLevel;
    string saveFile = "save";

    void OnTriggerEnter(Collider collider)
    {
        if (nextLevel != "MainMenu")
        {
            // Saves next level
            string saveText = "";

            using (StreamReader reader = new(Application.persistentDataPath + "/" + saveFile))
            {
                saveText = reader.ReadLine();

                string[] levels = new string[0];

                // Gets an array of levels from the save file
                if (saveText != null)
                {
                    levels = saveText.Split(',');
                }

                // If the next level has not been saved yet, save it
                if (!Array.Exists(levels, element => element == nextLevel))
                {
                    if (saveText != null)
                    {
                        saveText += ',';
                    }

                    saveText += nextLevel;
                }
            }

            using (StreamWriter writer = new(Application.persistentDataPath + "/" + saveFile))
                writer.Write(saveText);
        }

        // Loads next level
        SceneManager.LoadScene(nextLevel);
    }
}
