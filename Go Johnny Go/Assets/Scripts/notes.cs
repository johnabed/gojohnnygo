using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class notes : MonoBehaviour {
	[SerializeField]
	private float speed;

	private Rigidbody2D myRigidbody;

	private Vector2 direction;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate(){
		myRigidbody.velocity = direction * speed;
	}

	// Update is called once per frame
	void Update () {
	}

	public void Initialize(Vector2 direction){
		this.direction = direction;
	}

	void OnBecameInvisible(){
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D other){
		PlayerControl playerScript = FindObjectOfType<PlayerControl> ();
		UnityStandardAssets._2D.LevelManager levelManager = FindObjectOfType<UnityStandardAssets._2D.LevelManager>();

		if (other.tag == "Player" && playerScript.health > 0) {
			playerScript.health -= 1; //player loses health based on zombie damage
			if (playerScript.health <= 0) {
				levelManager.RespawnPlayer ();
			} else {
				playerScript.knockFromRight = true;
				playerScript.knockbackCount = playerScript.knockbackLength;
			}
			Destroy (gameObject);
		}
	}
}
