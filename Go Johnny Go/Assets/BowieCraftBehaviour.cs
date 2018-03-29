using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BowieCraftBehaviour : MonoBehaviour {

	private Vector3 posA;
	private Vector3 posB;
	private Vector3 posC;
	private Vector3 posD;
	private Vector3 nexPos;

	public bool start_triggered = false; 
	private bool player_triggered = false;
	[SerializeField]
	private float speed;
	[SerializeField]
	private Transform childTransform;
	[SerializeField]
	private Transform transformB;
	[SerializeField]
	private Transform transformC;
	[SerializeField]
	private Transform transformD;

	private GameObject player;
	private PlayerControl playerScript;
	private bool moveWithPlatform = false;
	public SceneFader fader;

	// Use this for initialization
	void Start () {
		Debug.Log ("start1");

		posA = childTransform.position;
		posB = transformB.position;
		posC = transformC.position;
		posD = transformD.position;
		nexPos = posA;
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.tag == "Player") {
			player_triggered = true;
			moveWithPlatform = true;

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

			nexPos = posA;
			moveWithPlatform = false;
		}
	}


	// Update is called once per frame
	void Update () {


		if (start_triggered) { 
			Move ();
		}
	}

	private void Move()
	{
		childTransform.position = Vector3.MoveTowards(childTransform.position, nexPos, speed * Time.deltaTime);

		if (moveWithPlatform) {
			player.transform.position = Vector3.MoveTowards(childTransform.position, nexPos, speed * Time.deltaTime);

		}
		if (Vector3.Distance(childTransform.position, nexPos) <= 0.1)
		{
			ChangeDestination();
		}
	}

	private void ChangeDestination()
	{
		if (nexPos == posA)
			nexPos = posB;
		if (nexPos == posB)
			nexPos = posC;
		if (nexPos == posC)
			nexPos = posD;
	}
		
}
