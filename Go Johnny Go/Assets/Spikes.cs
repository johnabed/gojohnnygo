using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
	private PlayerControl m_Character;


	void Start ()
	{
		m_Character = FindObjectOfType<PlayerControl> ();
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player") {
			bool knockFromRight = col.transform.position.x < transform.position.x ? true : false;
			m_Character.takeDamage (1, knockFromRight);
		}
	}

}