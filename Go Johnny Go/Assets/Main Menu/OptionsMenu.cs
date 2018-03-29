using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {
	
	public Slider volumeSlider;

	// Set volume 
	public void SetVolume (float volume) {
		AudioListener.volume = volume;

	}

	// Set graphics quality
	public void SetQuality (int qualityIndex){
		QualitySettings.SetQualityLevel (qualityIndex);
	}

	// Enable/Disable fullscreen
	public void SetFullScreen (bool isFullscreen) {
		Screen.fullScreen = isFullscreen;
	}
}
