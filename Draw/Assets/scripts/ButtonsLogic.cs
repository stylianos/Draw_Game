using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsLogic : MonoBehaviour {

    public string next_Level_Name;

    public void Go_To_Menu()
    {
        Debug.Log("Eimia i menui");
        next_Level_Name = "menu";
        LoadNextLevel();

    }
    public void New_Game()
    {
        next_Level_Name = "Level_1";
        LoadNextLevel();

    }

    public void Go_To_Info()
    {
        next_Level_Name = "info";
        LoadNextLevel();

    }
    
    public void Go_To_Credits()
    {
        next_Level_Name = "credits";
        LoadNextLevel();

    }

    public void LoadNextLevel()
    {
        StartCoroutine("Load");
    }

    IEnumerator Load()
    {
        //Don't let the Scene activate until you allow it to
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(next_Level_Name);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            Debug.Log("Loading progress: " + (asyncLoad.progress * 100) + "%");
            // Check if the load has finished
            if (asyncLoad.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
