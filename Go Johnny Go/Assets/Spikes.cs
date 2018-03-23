using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Spikes : MonoBehaviour
    {
		private PlayerControl m_Character;
        public LevelManager levelManager;


        void Start()
        {
			m_Character = FindObjectOfType<PlayerControl>();
            levelManager = FindObjectOfType<LevelManager>();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Player")
            {
                m_Character.health -= 1;
                if (m_Character.health == 0)
                {
                    levelManager.RespawnPlayer();
                }
                else
                {
                    Debug.Log(m_Character.health);
                    m_Character.knockbackCount = m_Character.knockbackLength;
                    if (col.transform.position.x < transform.position.x)
                    {
                        m_Character.knockFromRight = true;
                    }
                    else
                    {
                        m_Character.knockFromRight = false;
                    }
                }
            }
        }

    }
}