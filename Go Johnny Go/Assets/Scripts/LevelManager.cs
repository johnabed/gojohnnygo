using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class LevelManager : MonoBehaviour
    {

        public GameObject currentCheckpoint;
        private PlatformerCharacter2D player;


        // Use this for initialization
        void Start()
        {
            player = FindObjectOfType<PlatformerCharacter2D>();
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