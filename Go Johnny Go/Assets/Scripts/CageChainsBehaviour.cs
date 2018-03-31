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
	private GameObject bowieCraft;

	[SerializeField]
	private GameObject chains;


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
				foreach (BoxCollider2D coll in cage.GetComponents<BoxCollider2D> ()) {
					coll.enabled = false;
				}
					
				//Destroy (chains, 1);
				chains.SetActive(false);
				Destroy (cage, 10);
				bossScript.enabled = false;
				//StartCoroutine (SummonBowieCraft ());
				Invoke("triggerBowieCraft", 3);
				//StartCoroutine (EndGame ());
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

	//Called when Boss is defeated -
	IEnumerator EndGame()
	{
		while (true) {
			Debug.Log ("before wait");
			yield return new WaitForSeconds (3.0f); // wait before bowie craft comes up
			Debug.Log ("After wait");
			bowieCraft.GetComponent<BowieCraftBehaviour> ().start_triggered = true;

		}

	}
	private void triggerBowieCraft() {

		bowieCraft.GetComponent<BowieCraftBehaviour> ().start_triggered = true;

		}
			
	}




