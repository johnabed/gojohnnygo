using System.Collections;
using UnityEngine;
using System;

public class ZombieBehaviour : MonoBehaviour {

	public GameObject Player;
	public GameObject stopperA;
	public GameObject stopperB;
	public string Smart;
	public string Runner;

	private PlayerControl playerScript;
	private UnityStandardAssets._2D.LevelManager levelManager;

	private string walkDirection = "right";
	private bool flip = true;
	private bool move = true;
	private bool attack = true;

	public float timeBetweenAttacks, timeBetweenHits;
	private float dist, dist1, dist2, disty;
	private float attackRange;
	private float attackDistance;
	private float speed;
	public Animator animator;

	public int health;
	public int damage;

	private void Start()
	{
		animator = GetComponent<Animator> ();
		playerScript = FindObjectOfType<PlayerControl>();
		levelManager = FindObjectOfType<UnityStandardAssets._2D.LevelManager>();

		// If smart zombie, increase attack (detection) range
		if (Smart == "yes" || Smart == "y") {
			attackRange = 8;
		} else {
			attackRange = 2;
		}

		// Set running or walking type zombie
		if (Runner == "yes" || Runner == "y") {
			speed = 5f;
			attackDistance = 2.25f;
			health = 6;
			damage = 2;
			animator.SetBool ("isRunning", true);
		} else {
			speed = 2.5f;
			attackDistance = 0f;
			health = 3;
			damage = 1;
			animator.SetBool ("isWalking", true);
		}
	}
		
	// Update is called once per frame
	private void Update () {

		// Distance between the 2 stoppers
		dist1 = Vector3.Distance (stopperA.transform.position, transform.position);
		dist2 = Vector3.Distance (stopperB.transform.position, transform.position);

		// Distance between the zombie and the player
		dist = Player.transform.position.x - transform.position.x;
		disty = Player.transform.position.y - transform.position.y;

		// Start moving if player is far away
		if(Math.Abs(dist) > attackDistance && timeBetweenAttacks < 1.5)
		{
			move = true;
		}

		// Run attack animation if player is close
		else if (attackDistance != 0f && Math.Abs(dist) < attackDistance && Math.Abs(disty) < 3 && timeBetweenAttacks > 1.5)
		{
			animator.SetTrigger ("skill_1");
			timeBetweenAttacks = 0.0f;
			move = false;
			attack = true; // reset to true so zombie is allowed to attack again
		}

		// Player gets hit by sword attack if in range of sword
		if (attack == true && timeBetweenAttacks > 0.5 && move == false) {
			attack = false;			
			playerScript.health -= damage; //player loses health based on zombie damage
			if (playerScript.health <= 0) {
				levelManager.RespawnPlayer ();
			} else {
				playerScript.knockbackCount = playerScript.knockbackLength;
				if (Player.transform.position.x < transform.position.x) {
					playerScript.knockFromRight = true;
				} else {
					playerScript.knockFromRight = false;
				}
			}
		}

		// Timer that prevents animations from being spammed 
		timeBetweenAttacks += Time.deltaTime;
		timeBetweenHits += Time.deltaTime;

		// Walk if player is not in range of attack
		if (move == true && timeBetweenAttacks > 1.5) {
			// Change direction of the zombie when it reaches the end of the platform
			if (dist1 < 0.5 && flip == true) {

				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
				walkDirection = "right";
				flip = false;
			}

			else if (dist2 < 0.5 && flip == true) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
				walkDirection = "left";
				flip = false;
			}

			// Change flip back to true, flip prevents zombie from changing direction multiple times near a stopper
			if (dist1 > 1.5 && dist2 > 1.5)
				flip = true;

			// Walk in the direction it is facing
			if (walkDirection == "left") {
				Vector3 newPosition = transform.position;
				newPosition.x -= speed * Time.deltaTime;
				transform.position = newPosition;

			} else if (walkDirection == "right") {
				Vector3 newPosition = transform.position;
				newPosition.x += speed * Time.deltaTime;
				transform.position = newPosition;
			}
		}

		// Change behaviour of Player if player enters its range
		if (Math.Abs (dist) < attackRange && dist1 > 1.5 && dist2 > 1.5 && health > 0) {
			if (dist < 0) {
				if (walkDirection == "right") {
					transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
					flip = false;
				}
				walkDirection = "left";
			} else {
				if (walkDirection == "left") {
					transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
					flip = false;
				}
				walkDirection = "right";
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Note" && health > 0) {
			TakeDamage (playerScript.guitarDmg); //default case
			//This code block emits particles upon hitting the zombie
			ParticleSystem ps = other.GetComponentInChildren<ParticleSystem>();
			var sh = ps.shape;
			sh.radius = 5;
			ps.Emit(300);
			//Sets alpha to 0 so note is invisible
			other.GetComponent<SpriteRenderer> ().material.color = new Color (0,0,0,0);
			other.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			//Stops rendering particle simulator
			ps.Stop ();
			Destroy (other.gameObject,1);
		} else if (other.tag == "Player" && health > 0) {
			playerScript.health -= damage; //player loses health based on zombie damage
			if (playerScript.health <= 0) {
				levelManager.RespawnPlayer ();
			} else {
				playerScript.knockbackCount = playerScript.knockbackLength;
				if (other.transform.position.x < transform.position.x) {
					playerScript.knockFromRight = true;
				} else {
					playerScript.knockFromRight = false;
				}
			}
		}
	}

	// Function to apply damage to zombie
	public void TakeDamage (int damage)
	{
		if (timeBetweenHits > 0.5) {
			health -= damage;
			animator.SetTrigger ("hit_1");
			timeBetweenAttacks = 0.0f;
			move = false;

			if (health <= 0) {
				Dead ();
			}
		}
	}

	// Triggers when zombie's health reaches 0
	private void Dead()
	{
		animator.SetTrigger ("death");
		Destroy (this.gameObject, 1.5f);
	}
}
