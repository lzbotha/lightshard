using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	public float mainMenuButtonWidth = 200.0f;
	public float mainMenuButtonHeight = 50.0f;

	private int currentMenu = 0;
	private const int MAINMENUID = 0;
	private const int SETTINGSMENU = 1;
	private const int CREDITS = 2;

	void drawMainMenu(){
		if (GUI.Button(new Rect(Screen.width * 0.5f - mainMenuButtonWidth * 0.5f, 10, mainMenuButtonWidth, mainMenuButtonHeight), "Single Play")){
            Application.LoadLevel ("1-tutorial"); 
		}
		if(GUI.Button(new Rect(Screen.width * 0.5f - mainMenuButtonWidth * 0.5f, 60, mainMenuButtonWidth, mainMenuButtonHeight), "Cooperative Play")){
			Debug.Log("coop play");
		}
		if(GUI.Button(new Rect(Screen.width * 0.5f - mainMenuButtonWidth * 0.5f, 110, mainMenuButtonWidth, mainMenuButtonHeight), "Settings")){
			currentMenu = SETTINGSMENU;
		}
		if(GUI.Button(new Rect(Screen.width * 0.5f - mainMenuButtonWidth * 0.5f, 160, mainMenuButtonWidth, mainMenuButtonHeight), "Credits")){
			currentMenu = CREDITS;
		}
	}

	void drawCredits(){
		if(GUI.Button(new Rect(Screen.width * 0.5f - mainMenuButtonWidth * 0.5f, 160, mainMenuButtonWidth, mainMenuButtonHeight), "Back")){
			currentMenu = MAINMENUID;
		}
	}

	void drawSettings(){
		if(GUI.Button(new Rect(Screen.width * 0.5f - mainMenuButtonWidth * 0.5f, 160, mainMenuButtonWidth, mainMenuButtonHeight), "Back")){
			currentMenu = MAINMENUID;
		}
	}

	void OnGUI(){
		if(currentMenu == MAINMENUID){
			drawMainMenu();
		}
		else if (currentMenu == SETTINGSMENU){
			drawSettings();
		}
		else if (currentMenu == CREDITS){
			drawCredits();
		}
	}
}
