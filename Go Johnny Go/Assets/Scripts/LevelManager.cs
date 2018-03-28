using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D
{
	[RequireComponent(typeof(PlayerControl))]
    public class LevelManager : MonoBehaviour
    {

        public GameObject currentCheckpoint;
		public GameObject gameOverMenuUI;
		private PlayerControl player;


        // Use this for initialization
        void Start()
        {
			player = FindObjectOfType<PlayerControl>();
        }

        public void RespawnPlayer()
        {
            Debug.Log("Player: Respawn");
			//death animation here
			player.lives--;
			if (player.lives == 0) {
				Debug.Log ("GAME OVER");
				PlayerPrefs.DeleteKey ("Lives");
				PlayerPrefs.DeleteKey ("Health");
				gameOverMenuUI.SetActive(true);
				Time.timeScale = 0f;
			} else {
				player.health = 10;
				Vector3 checkpointPosition = new Vector3 (currentCheckpoint.transform.position.x, currentCheckpoint.transform.position.y + 5, 
					                             currentCheckpoint.transform.position.z);
				player.transform.position = checkpointPosition;
			}
        }
    }
}