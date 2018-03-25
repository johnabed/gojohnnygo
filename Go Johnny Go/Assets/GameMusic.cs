using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour {

// kill menu music

	// Use this for initialization
	void Awake()
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("music");

		Destroy (objs[0]);

	}
}
