using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

	public int coins;
	public int lives;
	public int health;
	private int MAX_HEALTH;
	public int guitarType;

	public float knockback;
	public float knockbackLength;
	public float knockbackCount;
	public bool knockFromRight;

	[SerializeField]
	private TextMeshProUGUI coinsText;

	[SerializeField]
	private TextMeshProUGUI livesText;

	[SerializeField]
	private Image healthUI;

	private SpriteRenderer guitar_renderer;
	private SpriteRenderer note_renderer;
	public Sprite guitarA;
	public Sprite guitarB;
	public int guitarDmg;

	public AudioClip MusicNoteClip;
	public AudioSource MusicNoteSource;

	public AudioClip CoinCollectClip;
	public AudioSource CoinCollectSource;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		facingRight = true;
		myAnimator = GetComponent<Animator>();

		coins = PlayerPrefs.GetInt("MoneyAmount");
		lives = PlayerPrefs.GetInt("Lives");
		if (lives == 0)
			lives = 3;
		MAX_HEALTH = 10;
		health = PlayerPrefs.GetInt("Health");
		if (health == 0)
			health = 10;

		//selecting guitartype for player sprite and damage
		guitarType = PlayerPrefs.GetInt ("GuitarType");
		guitar_renderer = guitar.GetComponent<SpriteRenderer> ();
		note_renderer = notePrefab.GetComponent<SpriteRenderer> ();

		if (guitarType == 0) {
			guitar_renderer.sprite = guitarA;
			guitarDmg = 1;
			note_renderer.color = Color.blue;
		} else {
			guitar_renderer.sprite = guitarB;
			guitarDmg = 2;
			note_renderer.color = Color.red;
		}

		MusicNoteSource.clip = MusicNoteClip;
		CoinCollectSource.clip = CoinCollectClip;
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis ("Horizontal");
		isGrounded = IsGrounded ();

		HandleMovement (horizontal);
		flip (horizontal);

		HandleLayers ();
	}

	void FixedUpdate () {
		coins = PlayerPrefs.GetInt("MoneyAmount");
		UpdateHUD ();
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
			StrumNote ();
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

	public void StrumNote(){
		MusicNoteSource.Play ();
		GameObject temp = (GameObject)Instantiate (notePrefab, guitar.GetComponentInChildren<ParticleSystem>().transform.position, Quaternion.identity);

		if (facingRight) {
			temp.GetComponent<notes> ().Initialize(Vector2.right);
		} else {
			temp.GetComponent<notes> ().Initialize(Vector2.left);
		}
	}

	public void UpdateHUD(){
		coinsText.text = "x" + coins.ToString ();
		livesText.text = "x" + lives.ToString ();
		healthUI.fillAmount = (float)health / (float)MAX_HEALTH;
	}

	public void coinCollect(int amount) {
		CoinCollectSource.Play ();
		coins += amount;
		PlayerPrefs.SetInt ("MoneyAmount", coins);
	}
}
