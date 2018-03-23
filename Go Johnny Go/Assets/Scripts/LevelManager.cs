using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            player.health = 10;
            player.transform.position = currentCheckpoint.transform.position;
        }
    }
}