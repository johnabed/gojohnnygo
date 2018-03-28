using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class KillPlayer : MonoBehaviour
    {

		public PlayerControl playerControl;

        // Use this for initialization
        void Start()
        {
			playerControl = FindObjectOfType<PlayerControl>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name == "Player")
            {
				playerControl.playerDeath ();
            }
        }
    }
}