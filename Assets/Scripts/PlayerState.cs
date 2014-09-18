using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour {
	public CharacterState characterState;
	public int player = 1;

	void Start(){
		if(player == 1)
			characterState.setPlayerTag ("Player 1 - ");
		else
			characterState.setPlayerTag ("Player 2 - ");
	}
}
