using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	public float mainMenuButtonWidth = 200.0f;
	public float mainMenuButtonHeight = 50.0f;

	private int currentMenu = 0;
	private const int MAINMENUID = 0;
	private const int SETTINGSMENU = 1;
	private const int CREDITS = 2;

	private string[] mainMenuButtons = new string[5] {"Single Play", "Cooperative Play", "Option", "Credits", "Exit"};
	private string[] mainMenuClicks = new string[5] {"onSinglePlayClick", "onCooperativePlayClick", "onOptionsClick", "onCreditsClick", "onExitClick"};

	void drawMainMenu(){
		int down = 250;
		for(int i = 0; i < mainMenuButtons.Length; i++){
			drawButton(
				mainMenuButtons[i],
				Screen.width * 0.5f - mainMenuButtonWidth * 0.5f,
				down,
				mainMenuButtonWidth,
				mainMenuButtonHeight,
				mainMenuClicks[i]
			);
			down += 60;
		}
	}
	
	// TODO: change the last term to a lambda
	private void drawButton(string name, float x, float y, float width, float height, string onClickMethodName){
		GUI.SetNextControlName(name);
		if (GUI.Button(new Rect(x, y, width, height), name)){
            Invoke(onClickMethodName, 0);
		}
	}

	private void onSinglePlayClick() {
		Application.LoadLevel ("1-tutorial"); 
	}

	private void onCooperativePlayClick() {
		Debug.Log("coop play");
	}

	private void onOptionsClick(){
		currentMenu = SETTINGSMENU;
	}

	private void onCreditsClick(){
		currentMenu = CREDITS;
	}

	private void onExitClick(){
		// NOTE: "Quit is ignored in the editor or the web player"  from Unity scripting API
		Application.Quit();
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
