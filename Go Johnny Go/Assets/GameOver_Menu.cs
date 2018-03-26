using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver_Menu : MonoBehaviour {

	// Update is called once per frame
	void Update () {
	}

    public void LevelSelect()
    {
		Time.timeScale = 1f;
		PlayerPrefs.DeleteKey ("Lives");
		PlayerPrefs.DeleteKey ("Health");
		SceneManager.LoadScene("Level Select");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
		PlayerPrefs.DeleteKey ("Lives");
		PlayerPrefs.DeleteKey ("Health");
        SceneManager.LoadScene("Main Menu");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game...");
		PlayerPrefs.DeleteAll ();
        Application.Quit();
    }
}
