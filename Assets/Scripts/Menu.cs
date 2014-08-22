using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	public float mainMenuButtonWidth = 200.0f;
	public float mainMenuButtonHeight = 50.0f;

	private int currentMenu = 0;
	private const int MAINMENUID = 0;
	private const int SETTINGSMENU = 1;
	private const int CREDITS = 2;

	private int currentFocusId = 0;

	private bool wasUpButtonDown = false;
	private bool wasDownButtonDown = false;
	private bool wasLeftButtonDown = false;
	private bool wasRightButtonDown = false;

	private bool shouldClick = false;

	private string[] mainMenuButtons = new string[5] {"Single Play", "Cooperative Play", "Option", "Credits", "Exit"};
	private string[] mainMenuClicks = new string[5] {"onSinglePlayClick", "onCooperativePlayClick", "onOptionsClick", "onCreditsClick", "onExitClick"};

	void drawMainMenu(){
		int down = 10;
		for(int i = 0; i < mainMenuButtons.Length; i++){
			drawButton(mainMenuButtons[i], Screen.width * 0.5f - mainMenuButtonWidth * 0.5f, down, mainMenuButtonWidth, mainMenuButtonHeight, mainMenuClicks[i]);
			down += 50;
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
		currentFocusId = -1;
		currentMenu = SETTINGSMENU;
	}

	private void onCreditsClick(){
		currentFocusId = -1;
		currentMenu = CREDITS;
	}

	private void onExitClick(){
		Debug.Log("There is no leaving... your soul is ours");
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
			
			currentFocusId += mainMenuButtons.Length;
			currentFocusId %= mainMenuButtons.Length;
			GUI.FocusControl(mainMenuButtons[currentFocusId]);

			if(shouldClick){
				shouldClick = false;
				Invoke(mainMenuClicks[currentFocusId] ,0);
			}
		}
		else if (currentMenu == SETTINGSMENU){
			drawSettings();
		}
		else if (currentMenu == CREDITS){
			drawCredits();
		}
	}

	void Update(){
		if(isUpButtonDown()){
			currentFocusId--;
		}
		if(isDownButtonDown()){
			currentFocusId++;
		}
		if(Input.GetButtonDown("Jump")){
			shouldClick = true;
		}
	}

	bool isUpButtonDown(){
		if(!wasUpButtonDown && Input.GetAxis("DpadVertical") > 0){
			wasUpButtonDown = true;
			return true;
		} else if (wasUpButtonDown && Input.GetAxis("DpadVertical") <= 0){
			wasUpButtonDown = false;
		}
		return false;
	}

	bool isDownButtonDown(){
		if(!wasDownButtonDown && Input.GetAxis("DpadVertical") < 0){
			wasDownButtonDown = true;
			return true;
		} else if (wasDownButtonDown && Input.GetAxis("DpadVertical") >= 0){
			wasDownButtonDown = false;
		}
		return false;
	}

	bool isRightButtonDown(){
		if(!wasRightButtonDown && Input.GetAxis("DpadHorizontal") > 0){
			wasRightButtonDown = true;
			return true;
		} else if (wasRightButtonDown && Input.GetAxis("DpadHorizontal") <= 0){
			wasRightButtonDown = false;
		}
		return false;
	}

	bool isLeftButtonDown(){
		if(!wasLeftButtonDown && Input.GetAxis("DpadHorizontal") < 0){
			wasLeftButtonDown = true;
			return true;
		} else if (wasLeftButtonDown && Input.GetAxis("DpadHorizontal") >= 0){
			wasLeftButtonDown = false;
		}
		return false;
	}
}
