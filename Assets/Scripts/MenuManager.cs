using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject WindowExit;
    bool exiting;

    public void Play(string scene)
    {
        if (!exiting) SceneManager.LoadScene(scene);
    }

    public void OpenWindow()
    {
        WindowExit.SetActive(true);
        exiting = true;
    }

    public void CloseWindow()
    {
        WindowExit.SetActive(false);
        exiting = false;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
