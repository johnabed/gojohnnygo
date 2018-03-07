using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlinkTMP : MonoBehaviour {

	public TMP_Text text;
	public float last, current;
	public bool state;

	void Start()
	{
		state = true;
		last = Time.time;
		text = GetComponent<TMP_Text> ();
		text.color = Color.white;
	}

	void Update()
	{
		current = Time.time;
		if (current > last + 0.4) {
			state = !state;
			last = current;
			if (text.color == Color.white)
				text.color = Color.clear;
			else
				text.color = Color.white;
		}
	}
}