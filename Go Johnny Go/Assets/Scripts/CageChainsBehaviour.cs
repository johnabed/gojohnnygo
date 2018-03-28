using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CageChainsBehaviour : MonoBehaviour {

	[SerializeField]
	private Image bossHealthbar;
	private int bossHealth;
	private int bossMaxHealth;

	[SerializeField]
	private GameObject cage;
	[SerializeField]
	private GameObject chains;

	void Start() {
		bossHealth = 100;
		bossMaxHealth = 100;
	}

	void OnTriggerEnter2D(Collider2D other){
		PlayerControl playerScript = FindObjectOfType<PlayerControl> ();

		if (other.tag == "Note" && bossHealth > 0) {
			bossHealth -= playerScript.guitarDmg;
			bossHealthbar.fillAmount = (float)bossHealth / (float)bossMaxHealth;
			if (bossHealth <= 0) {
				Debug.Log ("GAME WON");
				Destroy (chains);
			}
			Destroy (other);
		}
	}

}
