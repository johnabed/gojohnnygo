using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

	public Transform Background1;
	public Transform Background2;


	private bool whichBackground = true;


	public Transform cam;

	private float backgroundSize = 38.36f;
	private float currentPos = 38.36f;



	
	// Update is called once per frame
	void Update () {


		if (currentPos < cam.position.x) {

			if (whichBackground)
				Background1.localPosition = new Vector3 (Background1.localPosition.x + (2 * backgroundSize), Background1.localPosition.y, Background1.localPosition.z);
			else
				Background2.localPosition = new Vector3 (Background2.localPosition.x + (2 * backgroundSize), Background2.localPosition.y,  Background2.localPosition.z);

			currentPos += backgroundSize;

			whichBackground = !whichBackground;

		}
		if (currentPos > cam.position.x + backgroundSize) {

			if (whichBackground)
				Background2.localPosition = new Vector3 (Background2.localPosition.x - (2 * backgroundSize), Background2.localPosition.y,  Background2.localPosition.z);
			else
				Background1.localPosition = new Vector3 (Background1.localPosition.x - (2 * backgroundSize), Background1.localPosition.y,  Background1.localPosition.z);

			currentPos -= backgroundSize;

			whichBackground = !whichBackground;

		}
		
	}
}
