using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver_Menu : MonoBehaviour
{

	[SerializeField]
	private Image devil;
	[SerializeField]
	private GameObject gameOverMenuUI;

	void Start ()
	{
		enabled = false;
	}

	void Update ()
	{
		float alpha = devil.color.a;
		if (alpha < 1.00f) {
			alpha += 0.002f;
			devil.color = new Color (devil.color.r, devil.color.g, devil.color.b, alpha);
		}

	}

	public void LevelSelect ()
	{
		Time.timeScale = 1f;
		PlayerPrefs.DeleteKey ("Lives");
		PlayerPrefs.DeleteKey ("Health");
		SceneManager.LoadScene ("Level Select");
	}

	public void LoadMenu ()
	{
		Time.timeScale = 1f;
		PlayerPrefs.DeleteKey ("Lives");
		PlayerPrefs.DeleteKey ("Health");
		SceneManager.LoadScene ("Main Menu");
	}

	public void ExitGame ()
	{
		Debug.Log ("Exiting game...");
		PlayerPrefs.DeleteAll ();
		Application.Quit ();
	}
}
