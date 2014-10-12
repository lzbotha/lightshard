using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	public float mainMenuButtonWidth = 200.0f;
	public float mainMenuButtonHeight = 50.0f;

	private int currentMenu = 0;
	private const int MAINMENUID = 0;
	private const int LEVELMENU = 1;
	private const int CREDITS = 2;

	private string[] mainMenuButtons = new string[4] {"Single Play", "Cooperative Play", "Credits", "Exit"};
	private string[] mainMenuClicks = new string[4] {"onSinglePlayClick", "onCooperativePlayClick", "onCreditsClick", "onExitClick"};


	// TODO: change the last term to a lambda
	private void drawButton(string name, float x, float y, float width, float height, string onClickMethodName){
		GUI.SetNextControlName(name);
		if (GUI.Button(new Rect(x, y, width, height), name)){
            Invoke(onClickMethodName, 0);
		}
	}
	private void drawLevelMenu() {
		int down = 20;

		drawButton(
			"Tutorial",
		    Screen.width * 0.5f - mainMenuButtonWidth * 0.5f,
			down,
			mainMenuButtonWidth,
			mainMenuButtonHeight,
			"onTutorialClick"
		);
		down += 50;
		drawButton(
			"Level 1",
			Screen.width * 0.5f - mainMenuButtonWidth * 0.5f,
			down,
			mainMenuButtonWidth,
			mainMenuButtonHeight,
			"onLevel1Click"
			);
		
		down += 50;
		drawButton(
			"Level 2",
			Screen.width * 0.5f - mainMenuButtonWidth * 0.5f,
			down,
			mainMenuButtonWidth,
			mainMenuButtonHeight,
			"onLevel2Click"
			);
		
		down += 50;
		drawButton(
			"Level 3",
			Screen.width * 0.5f - mainMenuButtonWidth * 0.5f,
			down,
			mainMenuButtonWidth,
			mainMenuButtonHeight,
			"onLevel3Click"
			);
		
		down += 50;
		drawButton(
			"Back",
			Screen.width * 0.5f - mainMenuButtonWidth * 0.5f,
			down,
			mainMenuButtonWidth,
			mainMenuButtonHeight,
			"onBackClick"
			);

	}
	
	private void onTutorialClick() {
		Application.LoadLevel ("0-tutorial");
	}
	private void onLevel1Click() {
		Application.LoadLevel ("1-level");
	}
	private void onLevel2Click() {
        Application.LoadLevel("2-level");
	}
	private void onLevel3Click() {
        Application.LoadLevel("3-level");
	}


	private void onSinglePlayClick() {
		PlayerPrefs.SetInt ("isCoop", 0);
		currentMenu = LEVELMENU;
	}

	private void onCooperativePlayClick() {
		PlayerPrefs.SetInt ("isCoop", 1);
		currentMenu = LEVELMENU;
	}

	private void onCreditsClick(){
		currentMenu = CREDITS;
	}

	private void onExitClick(){
		// NOTE: "Quit is ignored in the editor or the web player"  from Unity scripting API
		Application.Quit();
	}

	void drawCredits(){

		float creditsWidth = Screen.width * 0.15f;
		float creditsCenter = Screen.width * 0.5f;

		float creditsTopLeftX = creditsCenter - creditsWidth / 2;
		
		float creditsHeight = Screen.height * 0.4f;
		float creditsHeightCenter = Screen.height * 0.3f;
		float creditsTopLeftY = creditsHeightCenter - creditsHeight / 2;

		var rect = new Rect (creditsTopLeftX, creditsTopLeftY, creditsWidth, creditsHeight);

		GUI.TextArea (rect, "Leonard Botha\nPierre Hugo\nEduardo Koloma\n\nDarren Brown\nRianelico D'Almeido\n\nDuncan Johnson\nSeirin Wi\nLucy Strauss");

		drawButton ("Back", (creditsWidth * 0.5f) + creditsTopLeftX - mainMenuButtonWidth / 2, Screen.height * 0.1f + creditsHeight + creditsTopLeftY, mainMenuButtonWidth, mainMenuButtonHeight, "onBackClick");
	}

	void onBackClick() {
		currentMenu = MAINMENUID;
	}

	void drawMainMenu(){
		int down = 20;
		for(int i = 0; i < mainMenuButtons.Length; i++){
			drawButton(
				mainMenuButtons[i],
				Screen.width * 0.5f - mainMenuButtonWidth * 0.5f,
				down,
				mainMenuButtonWidth,
				mainMenuButtonHeight,
				mainMenuClicks[i]
				);
			down += 50;
		}
	}

	void OnGUI(){
		if(currentMenu == MAINMENUID){
			drawMainMenu();
		}
		else if (currentMenu == CREDITS){
			drawCredits();
		} else if(currentMenu == LEVELMENU) {
			drawLevelMenu();
		} else {
			throw new System.Exception("Invalid menu id.");
		}
	}
}
