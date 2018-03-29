using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D
{
	[RequireComponent (typeof(PlayerControl))]
	public class LevelManager : MonoBehaviour
	{

		public GameObject currentCheckpoint;
		public GameObject gameOverMenuUI;
		private PlayerControl player;

		// Use this for initialization
		void Start ()
		{
			player = FindObjectOfType<PlayerControl> ();
		}

		public void RespawnPlayer ()
		{
			Debug.Log ("PLAYER RESPAWN");
			Vector3 checkpointPosition = new Vector3 (currentCheckpoint.transform.position.x, 
				                             currentCheckpoint.transform.position.y + 2, 
				                             currentCheckpoint.transform.position.z);
			player.transform.position = checkpointPosition;
			player.activateGuitar();
		}

		public void GameOver ()
		{
			Debug.Log ("GAME OVER");
			//enable gameover script and display
			gameOverMenuUI.SetActive (true);
			GameOver_Menu gom = FindObjectOfType<GameOver_Menu> ();
			gom.enabled = true;

			//disable pause menu script
			Pause_Menu pm = FindObjectOfType<Pause_Menu> ();
			pm.enabled = false; //disable pause menu script to avoid interaction
		}
	}
}