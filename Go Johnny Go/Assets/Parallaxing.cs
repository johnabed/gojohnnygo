using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

	public Transform[] backgrounds;			//Array of all the backgrounds that are going to get parallaxed
	private float[] parallaxScales; 		// the proportion of the camera's movement to move the backgrounds
	public float smoothing = 1f;			// smooth the parallax, (>0 always)

	private Transform cam;					// reference to the main camera's transform
	private Vector3 previousCamPos;			// position of the camera in previous frame


	// logic before start function
	void Awake () {
		// set up camera reference
		cam = Camera.main.transform;
	}

	// Use this for initialization
	void Start () {
		// store the previous frame 
		previousCamPos = cam.position;

		parallaxScales = new float[backgrounds.Length];

		// assignming parallax scales
		for (int i = 0; i < backgrounds.Length; i++) {
			parallaxScales[i] = backgrounds[i].position.z * -1;

		}
		
	}
	
	// Update is called once per frame
	void Update () {

		// for each background
		for (int i = 0; i < backgrounds.Length; i++) {
			// parallax is opposite of the camera movement because the prevoius frame multiplied by scale
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

			// set a target x position which is the current position plus the parallax
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;

			// create a target pos which is the backgrounds current position with its targets x pos
			Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			// fade between current pos and the target pos using lerp
			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}
		// set the previousCamPos to the camera's position at end of frame
		previousCamPos = cam.position; 
		
	}
}
