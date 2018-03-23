using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	[SerializeField]
	float movementSpeed = 10;

	private bool facingRight;

	[SerializeField]
	private Transform[] groundPoints;
	[SerializeField]
	private float groundRadius;
	[SerializeField]
	private LayerMask whatIsGround;
	private bool isGrounded;
	private bool jump;

	[SerializeField]
	private bool airControl = true;
	[SerializeField]
	private float jumpForce;

	[SerializeField]
	private GameObject notePrefab;
	[SerializeField]
	private GameObject guitar;

	private Animator myAnimator;
	private Rigidbody2D myRigidbody;

	public int health = 10;
	public float knockback;
	public float knockbackLength;
	public float knockbackCount;
	public bool knockFromRight;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		facingRight = true;
		myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float horizontal = Input.GetAxis ("Horizontal");
		isGrounded = IsGrounded ();

		HandleMovement (horizontal);
		flip (horizontal);

		HandleLayers ();
	}

	private void HandleMovement(float horizontal){
		if (myRigidbody.velocity.y < 0) {
			myAnimator.SetBool ("land", true);
		}
		if (isGrounded || airControl) {
			if (knockbackCount <= 0)
			{
				// Move the character
				myRigidbody.velocity = new Vector2 (horizontal * movementSpeed, myRigidbody.velocity.y);
			}
			else
			{
				if (knockFromRight)
				{
					myRigidbody.velocity = new Vector2(-knockback, knockback);
				} if (!knockFromRight)
				{
					myRigidbody.velocity = new Vector2(knockback, knockback);
				}
				knockbackCount -= Time.deltaTime;
			}

		}
		myAnimator.SetFloat ("speed", Mathf.Abs(horizontal));
		if (Input.GetKeyDown(KeyCode.UpArrow))
			jump = true;
		if (Input.GetKeyDown (KeyCode.Space)) {
			//myAnimator.SetTrigger ("strum");
			guitar.GetComponentInChildren<ParticleSystem>().Emit(500);
			//guitar.GetComponent<ParticleSystem>().Emit(500);
			StrumNote (0);
		}
		if (isGrounded && jump) {
			isGrounded = false;
			myRigidbody.AddForce (new Vector2 (0,jumpForce));
			jump = false;
			myAnimator.SetTrigger ("jump");
		}
	}

	private void flip (float horizontal){
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) {
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;

		}
	}

	private bool IsGrounded(){
		if (myRigidbody.velocity.y <= 0) {
			foreach (Transform point in groundPoints) {
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, whatIsGround);

				for (int i = 0; i < colliders.Length; i++) {
					if (colliders [i].gameObject != gameObject) {
						myAnimator.ResetTrigger ("jump");
						myAnimator.SetBool ("land", false);
						return true;
					}
				}
			}
		}
		return false;
	}

	private void HandleLayers(){
		if (!isGrounded) {
			myAnimator.SetLayerWeight (1, 1);
		} else {
			myAnimator.SetLayerWeight (1, 0);
		}
	}

	public void StrumNote(int value){
		GameObject temp = (GameObject)Instantiate (notePrefab, guitar.GetComponentInChildren<ParticleSystem>().transform.position, Quaternion.identity);
		if (facingRight) {
			temp.GetComponent<notes> ().Initialize(Vector2.right);
		} else {
			temp.GetComponent<notes> ().Initialize(Vector2.left);
		}
	}
}
