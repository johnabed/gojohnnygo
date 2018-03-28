using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
		temp.transform.localScale = new Vector3 (0.7f, 0.7f, 0);
	}

	void initializeNotes(){
		notes = new int[] {
			3,3,3,3,3,2,2,3,			//Didn't know what time it was and the lights were low
			3,3,3,3,3,2,2,4,			//I leaned back on my radio
			4,4,4,3,3,2,3,4,			//Some cat was layin' down some get it on rock 'n' roll, he said
			4,3,2,1,
			3,3,3,3,3,2,2,3,			//Then the loud sound did seem to fade
			3,3,3,3,3,2,2,4,			//Came back like a slow voice on a wave of phase haz
			4,4,4,3,2,2,1,2,			//That weren't no D.J. that was hazy cosmic jive
			4,4,4,3,4,4,4,2,
			4,4,1,1,2,2,1,2,			//CHORUS: There's a starman waiting in the sky
			2,3,3,3,2,3,3,2,			//He'd like to come and meet us, but he thinks he'd blow our minds
			4,4,1,1,2,2,1,2,			//There's a starman waiting in the sky
			2,3,3,3,2,2,1,1,			//He's told us not to blow it, cause he knows it's all worthwhile, He told me
			1,2,1,2,					//Let the children lose it
			2,3,2,3,					//Let the children use it
			4,4,2,3,					//Let all the children boogie
			2,2,3,4,4,4,3,2,
			1,1,3,4,4,4,3,2,
			2,2,3,4,4,4,3,2,
			1,1,3,4,
			3,3,3,3,3,2,2,3,			//I had to phone someone so I picked on you
			3,3,3,3,3,2,2,4,			//Hey, that's far out so you heard him too
			4,4,4,3,3,2,3,4,			//Switch on the TV we may pick him up on channel two
			4,3,2,1,
			3,3,3,3,3,2,2,3,			//Look out your window I can see his light
			3,3,3,3,3,2,2,4,			//If we can sparkle he may land tonight
			4,4,4,3,2,2,1,2,			//Don't tell your poppa or he'll get us locked up in fright
			 4,4,4,3,4,4,4,2,
			4,4,1,1,2,2,1,2,			//CHORUS: There's a starman waiting in the sky
			2,3,3,3,2,3,3,2,			//He'd like to come and meet us, but he thinks he'd blow our minds
			4,4,1,1,2,2,1,2,			//There's a starman waiting in the sky
			2,3,3,3,2,2,1,1,			//He's told us not to blow it, cause he knows it's all worthwhile, He told me
			1,2,1,2,					//Let the children lose it
			2,3,2,3,					//Let the children use it
			4,4,2,3,					//Let all the children boogie
			4,4,1,1,2,2,1,2,			//CHORUS: There's a starman waiting in the sky
			2,3,3,3,2,3,3,2,			//He'd like to come and meet us, but he thinks he'd blow our minds
			4,4,1,1,2,2,1,2,			//There's a starman waiting in the sky
			2,3,3,3,2,2,1,1,			//He's told us not to blow it, cause he knows it's all worthwhile, He told me
			1,2,1,2,					//Let the children lose it
			2,3,2,3,					//Let the children use it
			4,4,2,3,					//Let all the children boogie
			2,2,3,4,4,4,3,2,			//80-81
			1,1,3,4,4,4,3,2,			//82
			2,2,3,4,4,4,3,2,			//84
			1,1,3,4,4,4,3,2,			//86
			2,2,3,4,4,4,3,2,			//88
			1,1,3,4,4,4,3,2,			//90
			2,1,1,1,2,3,3,3,			//92
			1,3,3,4,4,3,2,2,			//94
			3,3,2,2,1,1,2,2,			//96
			3,4,2,3,4,3,2,2,			//98
			2,1,1,2,2,2,3,3,			//100
			4,4,3,3,4,4,3,3,			//102
			2,2,3,4,4,4,3,2,			//104
			};
	}
}
