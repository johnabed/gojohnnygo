using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControlScript : MonoBehaviour {

	public static int moneyAmount;

	// Use this for initialization
	void Start () {
		moneyAmount = PlayerPrefs.GetInt ("MoneyAmount");
		if (moneyAmount == 0)
			moneyAmount = 501;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void changeScene(string scene) {
		PlayerPrefs.SetInt("MoneyAmount", moneyAmount);
		SceneManager.LoadScene (scene);
	}

	public void quitGame() {
		PlayerPrefs.DeleteAll ();
		Debug.Log ("QUIT");
		Application.Quit ();
	}
}
