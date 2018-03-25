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
		private PlayerControl player;


        // Use this for initialization
        void Start()
        {
			player = FindObjectOfType<PlayerControl>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void RespawnPlayer()
        {
            Debug.Log("Player: Respawn");
			//death animation here
			player.lives--;
			if (player.lives == 0) {
				Debug.Log ("GAME OVER");
				SceneManager.LoadScene ("Level Select");
			} else {
				player.health = 10;
				Vector3 checkpointPosition = new Vector3 (currentCheckpoint.transform.position.x, currentCheckpoint.transform.position.y + 5, 
					                             currentCheckpoint.transform.position.z);
				player.transform.position = checkpointPosition;
			}
        }
    }
}