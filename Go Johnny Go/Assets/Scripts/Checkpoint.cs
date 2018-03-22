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
        // Use this for initialization
        void Start()
        {
            levelManager = FindObjectOfType<LevelManager>();
            sr = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name == "Player")
            {
                sr.sprite = newSprite;
                levelManager.currentCheckpoint = gameObject;
                GetComponent<CircleCollider2D>().enabled = false;
            }
        }
    }
}