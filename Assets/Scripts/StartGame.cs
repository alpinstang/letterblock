using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class StartGame : MonoBehaviour
{
    void LoadLevel(bool loadFile)
    {
        if(loadFile)
        {
            Debug.Log("Loading Game File...");
            LoadExistingGame();
        }
        SceneManager.LoadScene(1);
    }

    public void LoadExistingGame()
    {
        // TODO: Load settings and score from save file
    }

    public void Settings()
    {

    }
}
