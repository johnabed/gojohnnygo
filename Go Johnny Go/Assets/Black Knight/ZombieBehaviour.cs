using System.Collections;
using UnityEngine;
using System;

public class ZombieBehaviour : MonoBehaviour {

	public GameObject Player;
	public GameObject stopperA;
	public GameObject stopperB;
	public string Smart;

	private float speed=2.0f;
	private string walkDirection = "right";
	private bool flip = true;
	private bool walk = true;

	private float timeBetweenAttacks;
	private float dist, dist1, dist2;
	private float attackRange;
	private Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator> ();

		if (Smart == "yes" || Smart == "y") {
			attackRange = 9;
		} else {
			attackRange = 2;
		}
		animator.SetBool ("isWalking", true);
	}
		
	// Update is called once per frame
	void FixedUpdate () {

		// Distance between the 2 stoppers
		dist1 = Vector3.Distance (stopperA.transform.position, transform.position);
		dist2 = Vector3.Distance (stopperB.transform.position, transform.position);

		// Distance between the zombie and the player
		dist = Player.transform.position.x - transform.position.x;

		// Run walking animation if player is far away
		if(Math.Abs(dist) > 2 && timeBetweenAttacks < 1.25)
		{
			walk = true;
		}

		// Run attack animation if player is close
		else if (Math.Abs(dist) < 2 && timeBetweenAttacks > 1.25)
		{
			animator.SetTrigger ("skill_1");
			timeBetweenAttacks = 0.0f;
			walk = false;
		}

		// Timer that prevents animations from being spammed 
		timeBetweenAttacks += Time.deltaTime;

		// Walk if player is not in range of attack
		if (walk == true && timeBetweenAttacks > 1.25) {
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
}
