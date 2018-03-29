using UnityEngine;
using System.Collections;
public class BowieSpeechBubble : MonoBehaviour
{
	[SerializeField]
	Canvas messageCanvas;


	void Start()
	{
		messageCanvas.enabled = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.CompareTag("bowiecraft"))
		{
			TurnOnMessage();
		}
	}

	private void TurnOnMessage()
	{
		messageCanvas.enabled = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("bowiecraft"))
		{
			TurnOffMessage();
		}
	}

	private void TurnOffMessage()
	{
		messageCanvas.enabled = false;
	}
}