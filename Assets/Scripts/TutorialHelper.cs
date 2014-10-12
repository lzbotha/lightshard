using UnityEngine;
using System.Collections;

public class TutorialHelper : MonoBehaviour {
	public const int BEFORE = 0;
	public const int DURING = 1;
	public const int AFTER = 2;
	
	public int jumpState = BEFORE;
	public int flashState = BEFORE;
	public int smearState = BEFORE;
	public int bonfireState = BEFORE;
	
	void jump() {
		if (jumpState == DURING) {
			float w = 135.0f;
			GUI.TextArea(new Rect(Screen.width / 2 - w / 2, Screen.height * 0.8f,
			                      w, 20),
			             "Press A to Jump");
			
			if (Input.GetButton("Player 1 - Jump")) {
				jumpState = AFTER;
			}
		}
	}

	void smear() {
		if (smearState == DURING) {
			float w = 135.0f;
			GUI.TextArea(new Rect(Screen.width / 2 - w / 2, Screen.height * 0.8f,
			                      w, 20),
			             "Press B to Smear");
			
			if (Input.GetButton("Player 1 - Smear")) {
				Debug.Log ("Smear after");
				smearState = AFTER;
			}
		}
	}
	
	void flash() {
		if (flashState == DURING) {
			float w = 135.0f;
			GUI.TextArea(new Rect(Screen.width / 2 - w / 2, Screen.height * 0.8f,
			                      w, 20),
			             "Press Y to Flash");
			
			if (Input.GetButton("Player 1 - Flash")) {
				flashState = AFTER;
			}
		}
	}
	
	void bonfire() {
		if (bonfireState == DURING) {
			float w = 150.0f;
			GUI.TextArea(new Rect(Screen.width / 2 - w / 2, Screen.height * 0.8f,
			                      w, 20),
			             "Light the bonfire to win!");

		}
	}

	// Update is called once per frame
	void OnGUI () {
		jump ();
		flash ();
		smear ();
		bonfire ();
	}
}
