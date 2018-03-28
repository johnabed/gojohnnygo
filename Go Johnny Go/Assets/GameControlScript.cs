using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControlScript : MonoBehaviour {

	public void changeScene(string scene) {
		SceneManager.LoadScene (scene);
	}

	public void quitGame() {
		Debug.Log ("QUIT");
		PlayerPrefs.DeleteAll ();
		Application.Quit ();
	}
}
