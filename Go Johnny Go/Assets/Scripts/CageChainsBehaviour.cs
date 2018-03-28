using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CageChainsBehaviour : MonoBehaviour {

	[SerializeField]
	private Image bossHealthbar;

	private Vector3 cagePos;

	[SerializeField]
	private GameObject cage;
	[SerializeField]
	private GameObject chains;
	[SerializeField]
	private SceneFader fader;

	void Start() {
		cagePos = cage.transform.position;
	}

	void OnTriggerEnter2D(Collider2D other){
		PlayerControl playerScript = FindObjectOfType<PlayerControl> ();
		BossController bossScript = FindObjectOfType<BossController> ();

		if (other.tag == "Note" && bossScript.health > 0) {
			other.GetComponent<PolygonCollider2D>().enabled = false; //disable further collisions from Note
			bossScript.health -= playerScript.guitarDmg;
			bossHealthbar.fillAmount = (float)bossScript.health / (float)bossScript.MAX_HEALTH;
			if (bossScript.health <= 0) {
				Debug.Log ("GAME WON");
				cage.transform.position = cagePos;
				Destroy (chains, 1);
				StartCoroutine (EndGame ());
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

	//Called when Boss is defeated - aiden you would put your sequence here
	IEnumerator EndGame()
	{
		while (true) {
			yield return new WaitForSeconds(3.0f); //pauses function for 3 seconds
			PlayerPrefs.DeleteKey ("Health"); //reset these fields to normal values
			PlayerPrefs.DeleteKey ("Lives");
			fader.FadeTo("Ending Scene"); //go to Ending Scene
		}
	}

}
