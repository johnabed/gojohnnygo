using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

	public int offsetX = 2;		// offset so we don't get any weird errors

	// these are used for checking if we need to instantiate stuff
	public bool hasARightBuddy = false;
	public bool hasALeftBuddy = false;

	public bool reverseScale = false; //used if the object is not tilable

	//public Transform parents;

	private float spriteWidth = 0f;		// the width of our element

	private Camera cam;
	private Transform myTransform;


	void Awake () {
		cam = Camera.main;
		myTransform = transform;

	}

	// Use this for initialization
	void Start () {

		SpriteRenderer sRenderer = GetComponent<SpriteRenderer> ();
		spriteWidth = sRenderer.sprite.bounds.size.x;
		
	}
	
	// Update is called once per frame
	void Update () {

		// does it still need buddes, if not do nothing
		if (!hasALeftBuddy || !hasARightBuddy) {
			// calculate the cameras extend (half width) of what the camera can see in world coords
			float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;

			// calculate the x pos where the camera can see the edge of the sprite
			float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth/2) - camHorizontalExtend;
			float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtend;

			// check to see if we can see edge of element & call make new buddy if we can
			if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false) {
				MakeNewBuddy (1);
				hasARightBuddy = true;

			} else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasALeftBuddy == false) {
				MakeNewBuddy (-1);
				hasALeftBuddy = true;

			}
		}
		
	}

	// creates a buddy on right or left side
	void MakeNewBuddy(int rightOrLeft) {

		//calculating the new position for our new buddy
		Vector3 newPosition = new Vector3 (myTransform.position.x + myTransform.localScale.x * spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);

		// store new buddy
		Transform newBuddy = Instantiate (myTransform, newPosition, myTransform.rotation) as Transform;

		// if its not tilable lets reverse the x size of the object to get rid of seams
		if (reverseScale) {
			newBuddy.localScale = new Vector3 (newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
		}

		//newBuddy.SetParent (myTransform);

		newBuddy.parent = myTransform.parent;
		//newBuddy.transform.parent = parents.transform;
		//newBuddy.localScale = new Vector3 (1, 1, 1);

		if (rightOrLeft > 0) {
			newBuddy.GetComponent<Tiling> ().hasALeftBuddy = true;
		} else
			newBuddy.GetComponent<Tiling> ().hasARightBuddy = true;
	}
}
