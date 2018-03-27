using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class notes : MonoBehaviour {
	[SerializeField]
	private float speed;

	private Rigidbody2D myRigidbody;

	private Vector2 direction;

	private UnityStandardAssets._2D.LevelManager levelManager;
	private PlayerControl playerScript;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();

		playerScript = FindObjectOfType<PlayerControl>();
		levelManager = FindObjectOfType<UnityStandardAssets._2D.LevelManager>();
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
}
