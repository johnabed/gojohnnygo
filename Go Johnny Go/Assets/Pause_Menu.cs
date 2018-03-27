using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour {
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    string menu = "Main Menu";

	public AudioSource songSrc;
	public AudioClip songClip;

	void Start () {
		songSrc.clip = songClip;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
				songSrc.UnPause ();
                Resume();
            }
            else
            {
				songSrc.Pause ();
                Pause();
            }
        }
	}

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
		PlayerPrefs.DeleteKey ("Lives");
		PlayerPrefs.DeleteKey ("Health");
        SceneManager.LoadScene(menu);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game...");
		PlayerPrefs.DeleteAll ();
        Application.Quit();
    }
}
