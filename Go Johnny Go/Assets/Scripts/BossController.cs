using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

	[SerializeField]
	private GameObject notePrefab;

	[SerializeField]
	private float noteTime = 25/30;

	[SerializeField]
	private float noteReleaseTime = 20;
	private float elapsedTime;
	private float initialTime;

	private int counter;			// Indexes how many notes have been released

	private int[] notes;

	private Vector3[] locations = new Vector3[4];
	private GameObject[] children;

	[SerializeField]
	private GameObject chains;
	[SerializeField]
	private GameObject player;

	// Use this for initialization
	void Start () {
		//children = this.GetComponentsInChildren<GameObject>();
		children = GameObject.FindGameObjectsWithTag("ObjectsPlatform");
		for (int i = 0; i < 4; i++) {
			//children [i] = GameObject.Find (i.ToString);
			int index = int.Parse (children [i].name);
			locations [index-1] = children [i].transform.position;
		}
		counter = 0;
		initializeNotes ();
		initialTime = Time.time;
		noteReleaseTime *= noteTime;
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime = Time.time - initialTime;
		if (elapsedTime >= noteReleaseTime) {
			noteReleaseTime += noteTime;
			if (counter < notes.Length) {
				releaseNote ();
				counter += 1;
			}
		}
	}

	void releaseNote(){
		int index = notes[counter];
		Vector3 spawnPosition = locations [index - 1];
		GameObject temp = (GameObject)Instantiate (notePrefab, spawnPosition, Quaternion.identity);
		temp.GetComponent<notes> ().Initialize(Vector2.left);
		temp.transform.localScale = new Vector3 (1, 1, 0);
	}

	void initializeNotes(){
		notes = new int[] {4,4,4,4,4,3,2,3,		//Didn't know what time it was and the lights were low
			4,4,4,4,3,4,2,3,2,4,				//I leaned back on my radio
			4,4,4,4,2,3,2,3,2,2,				//Some cat was layin' down some get it on rock 'n' roll, he said
			4,4,3,2,
			4,3,4,3,4,3,2,1,2,3,				//Then the loud sound did seem to fade
			2,4,3,3,3,4,3,2,1,4,4,4,1,2,
			3,4,3,3,4,3,1,2,3,4,3,2,1,2, // The sounds before the chorus
			4,4,4,4,4,4,4,4,3, // Chorus begins after this part
			2,1,4,1,2,2,1,1,2,1,2,1,1,1};
	}
}
