using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollZ : MonoBehaviour {

	public SceneFader fader;

	// variable letting us change how fast we'll move text
	public float scrollSpeed = 30;

	void Update () {
		// get current position of parent GameObject
		Vector3 pos = transform.position;
		if(transform.position.y >= 1000) {
			fader.FadeTo ("Main Menu"); //fade out when y reaches -300
		}
		else if(transform.position.y >= 600) {
			float alpha = GetComponent<MeshRenderer> ().material.color.a;
			if (alpha > 0.00f) {
				alpha -= 0.001f;
				GetComponent<MeshRenderer>().material.color = new Color(GetComponent<MeshRenderer>().material.color.r, 
					GetComponent<MeshRenderer>().material.color.g, GetComponent<MeshRenderer>().material.color.b, 
					alpha);
			}
		}

		// get vector pointing into the distance
		Vector3 localVectorUp = transform.TransformDirection(0,1,0);

		// move the text object into the distance to give our 3D scrolling effect
		pos += localVectorUp * scrollSpeed * Time.deltaTime;
		transform.position = pos;
	}
}
