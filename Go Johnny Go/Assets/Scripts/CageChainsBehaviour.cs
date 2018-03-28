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
				cage.transform.position = new Vector3 (15, -2, 0); //temp solution to swinging issue
				Debug.Log ("GAME WON");
				Destroy (chains);
			}
			//This code block emits particles upon hitting the zombie
			ParticleSystem ps = other.GetComponentInChildren<ParticleSystem>();
			var sh = ps.shape;
			sh.radius = 5;
			ps.Emit(300);
			//Sets alpha to 0 so note is invisible
			other.GetComponent<SpriteRenderer> ().material.color = new Color (0,0,0,0);
			//Stops rendering particle simulator
			ps.Stop ();
			Destroy (other.gameObject,1);
		}
	}

}
