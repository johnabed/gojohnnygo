using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets._2D
{
    public class Checkpoint : MonoBehaviour
    {

        public LevelManager levelManager;
        SpriteRenderer sr;
        public Sprite newSprite;

		public AudioClip CheckpointClip;
		public AudioSource CheckpointSource;

        // Use this for initialization
        void Start()
        {
            levelManager = FindObjectOfType<LevelManager>();
            sr = GetComponent<SpriteRenderer>();

			CheckpointSource.clip = CheckpointClip;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name == "Player")
            {
				CheckpointSource.Play ();
                sr.sprite = newSprite;
                levelManager.currentCheckpoint = gameObject;
                GetComponent<CircleCollider2D>().enabled = false;
            }
        }
    }
}