using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

	public Slider volumeSlider;
	public Dropdown resolutionDropdown;

	Resolution[] resolutions;

	void Start() {
		// Create a list of useable resolutions to select from in options menu
		resolutions = Screen.resolutions;
		resolutionDropdown.ClearOptions ();
		List<string> options = new List<string> ();
		int currentResolutionIndex = 0;

		for (int i = 0; i < resolutions.Length; i++) {
			string option = resolutions [i].width + " x " + resolutions [i].height;
			options.Add (option);

			// Get the index of the current in-game screen resolution
			if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
				currentResolutionIndex = i;
		}

		// Add all the options in the resolution list and set the resolution dropdown default value to the current game resolution
		resolutionDropdown.AddOptions (options);
		resolutionDropdown.value = currentResolutionIndex;
		resolutionDropdown.RefreshShownValue ();
	}

	// Set resolution
	public void SetResolution(int resolutionIndex) {
		Resolution resolution = resolutions [resolutionIndex];
		Screen.SetResolution (resolution.width, resolution.height, Screen.fullScreen);
	}

	// Set volume 
	public void SetVolume (float volume) {
		AudioListener.volume = volume;
	}

	// Enable/Disable fullscreen
	public void SetFullScreen (bool isFullscreen) {
		Screen.fullScreen = isFullscreen;
	}
}
