using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour {
	public CharacterState characterState;
	public Camera characterCamera;
	public int player = 1;
	public int playersInLevel = 1;

	void Start(){
		if (player == 1) {
			this.characterState.setPlayerTag ("Player 1 - ");
			if(this.playersInLevel == 2)
				this.characterCamera.rect = new Rect(0.0f, 0.5f, 1.0f, 0.5f);
			else
				this.characterCamera.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);

		} else {
			this.characterState.setPlayerTag ("Player 2 - ");
			this.characterCamera.rect = new Rect(0.0f, 0.0f, 1.0f, 0.5f);
		}
	}
}
