using UnityEngine;
using System.Collections;

public class InGameMenu : MonoBehaviour {

	public float buttonHeight = 75.0f;
	public float buttonWidth = 150.0f;
	public float gap = 10.0f;

	bool isPaused() {
		return Time.timeScale == 0;
	}

	void OnGUI () {
		if (this.isPaused()) {

			if(GUI.Button(new Rect((Screen.width - buttonWidth) * 0.5f , Screen.height * 0.5f - 2 * (buttonHeight + gap), buttonWidth, buttonHeight), "Continue")){
				Debug.Log("Button working");
				Time.timeScale = 1;
			}

			if(GUI.Button(new Rect((Screen.width - buttonWidth) * 0.5f , Screen.height * 0.5f - (buttonHeight + gap), buttonWidth, buttonHeight), "Quit to Main Menu")){

			}

			if(GUI.Button(new Rect((Screen.width - buttonWidth) * 0.5f , Screen.height * 0.5f, buttonWidth, buttonHeight), "Quit to Desktop")){
				
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Pause") && !isPaused())
			Time.timeScale = 0;
		else if(Input.GetButtonDown("Pause"))
			Time.timeScale = 1;
	}
}
