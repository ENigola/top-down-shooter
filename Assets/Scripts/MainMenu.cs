using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void ExitGame()
    {
        Debug.Log("APPLICATION QUIT");
        Application.Quit();
    }

    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
}
