using System.Collections;
using UnityEngine;
using System;

public class ZombieBehaviour : MonoBehaviour {

	public GameObject Player;
	public GameObject stopperA;
	public GameObject stopperB;
	public string Smart;
	public string Runner;

	private string walkDirection = "right";
	private bool flip = true;
	private bool move = true;

	private float timeBetweenAttacks, timeBetweenHits;
	private float dist, dist1, dist2, disty;
	private float attackRange;
	private float attackDistance;
	private float speed;
	private Animator animator;

	private int health;
	private int damage;

	private void Start()
	{
		animator = GetComponent<Animator> ();

		// If smart zombie, increase attack (detection) range
		if (Smart == "yes" || Smart == "y") {
			attackRange = 8;
		} else {
			attackRange = 2;
		}

		// Set running or walking type zombie
		if (Runner == "yes" || Runner == "y") {
			speed = 5f;
			attackDistance = 2f;
			health = 10;
			damage = 3;
			animator.SetBool ("isRunning", true);
		} else {
			speed = 2.5f;
			attackDistance = 1.25f;
			health = 5;
			damage = 1;
			animator.SetBool ("isWalking", true);
		}
	}
		
	// Update is called once per frame
	private void FixedUpdate () {

		// Distance between the 2 stoppers
		dist1 = Vector3.Distance (stopperA.transform.position, transform.position);
		dist2 = Vector3.Distance (stopperB.transform.position, transform.position);

		// Distance between the zombie and the player
		dist = Player.transform.position.x - transform.position.x;
		disty = Player.transform.position.y - transform.position.y;

		// Start moving if player is far away
		if(Math.Abs(dist) > attackDistance && timeBetweenAttacks < 1.55)
		{
			move = true;
		}

		// Run attack animation if player is close
		else if (Math.Abs(dist) < attackDistance && Math.Abs(disty) < 3 && timeBetweenAttacks > 1.55)
		{
			animator.SetTrigger ("skill_1");
			timeBetweenAttacks = 0.0f;
			move = false;
			// playerhealth -= damage;
		}

		// Timer that prevents animations from being spammed 
		timeBetweenAttacks += Time.deltaTime;
		timeBetweenHits += Time.deltaTime;


		// Walk if player is not in range of attack
		if (move == true && timeBetweenAttacks > 1.55) {
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
		if (Math.Abs (dist) < attackRange && dist1 > 1.5 && dist2 > 1.5) {
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
		/*
			} else if (atkTimes == 2) {
				animator.SetTrigger ("hit_2");
			} else if (atkTimes == 3) {
				animator.SetTrigger ("hit_2");
				animator.SetTrigger ("death");
			} */


	}

	// Function to apply damage to zombie
	public void ApplyDamage (int damage)
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
