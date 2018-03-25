using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleport : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){

		if(other.tag == "Player")
			//SceneManager.LoadScene ("boss_level", LoadSceneMode.Single);
			Debug.Log("hello");
	}

}
