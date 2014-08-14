using UnityEngine;
using System.Collections;

public class CharacterLightAbilities : MonoBehaviour {

	private CharacterState characterState;

	// Use this for initialization
	void Start () {
		characterState = gameObject.GetComponent<CharacterState>();
	}
	
	// Update is called once per frame
	void Update () {
		// Right/Left trigger is down
		if(Input.GetAxis("ThrowRight") > 0 || Input.GetAxis("ThrowLeft") > 0){
			// Lock the characters movementDirection
			characterState.movementDirectionLocked = true;
		} 
		// Right trigger has been released
		else if (characterState.movementDirectionLocked == true){
			// Unlock the characters movement direction
			characterState.movementDirectionLocked = false;
		}

	}
}
