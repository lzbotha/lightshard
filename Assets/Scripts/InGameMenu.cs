using UnityEngine;
using System.Collections;

public class InGameMenu : MonoBehaviour {

	bool isPaused() {
		return Time.timeScale == 0;
	}

	void OnGUI () {
		if (this.isPaused()) {
			if(GUI.Button(new Rect(Screen.width * 0.5f , 160, 100, 100), "Back")){
				Debug.Log("Button working");
				Time.timeScale = 1;
			}

			if (Input.GetButtonDown("Pause")) {
				Time.timeScale = 1;
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
