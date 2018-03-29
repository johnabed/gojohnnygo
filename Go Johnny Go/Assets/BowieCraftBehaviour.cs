using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BowieCraftBehaviour : MonoBehaviour {

	private Vector3 spacePos;
	private Vector3 pickUpPos;

	public bool start_triggered = false; 
	private bool player_triggered = false;
	[SerializeField]
	private float speed;
	[SerializeField]
	private Transform childTransform;
	[SerializeField]
	private Transform pickUpPoint;
	[SerializeField]
	private Transform flyToSpacePoint;

	[SerializeField]
	private SceneFader fader;


	private GameObject player;
	private PlayerControl playerScript;
	private bool moveWithSpaceship = false;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		spacePos = flyToSpacePoint.position;
		pickUpPos = pickUpPoint.position;
	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.tag == "Player") {
			start_triggered = false;
			player_triggered = true;
			moveWithSpaceship = true;

		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			player = GameObject.FindGameObjectWithTag ("Player");
			player.transform.parent = transform;

		}

	}

	void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			player.transform.parent = null;

			moveWithSpaceship = false;
		}
	}


	// Update is called once per frame
	void Update () {


		if (start_triggered) { 
			Move ();
		}
		if (player_triggered) {
			FlyToSpace ();
		}
	}

	private void FlyToSpace()
	{
		childTransform.position = Vector3.MoveTowards(childTransform.position, spacePos, speed * Time.deltaTime);

		if (moveWithSpaceship) {
			player.transform.position = Vector3.MoveTowards(childTransform.position, spacePos, speed * Time.deltaTime);

		}
			
		StartCoroutine (fadeToEndGame ());

	}

	private void Move()
	{
		childTransform.position = Vector3.MoveTowards(childTransform.position, pickUpPos, speed * Time.deltaTime);


	}

	private IEnumerator fadeToEndGame() {
		yield return new WaitForSeconds(10); //pauses function for 4 seconds
		PlayerPrefs.DeleteKey ("Health"); //reset these fields to normal values
		PlayerPrefs.DeleteKey ("Lives");
		fader.FadeTo("Ending Scene"); //go to Ending Scene

	}


		
}
