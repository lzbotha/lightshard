using UnityEngine;
using System.Collections;

public class CharacterLightAbilities : MonoBehaviour {

	public float flashLightRadiusIncrease = 2.0f;
	private CharacterState characterState;

	// Use this for initialization
	void Start () {
		characterState = gameObject.GetComponent<CharacterState>();
	}
	
	// Update is called once per frame
	void Update () {
		// If the player activates flash and there is no current flash active
		if(Input.GetButtonDown("Flash") && characterState.lightRegenRate > 0){
			characterState.lightRadius += flashLightRadiusIncrease;
			characterState.lightRegenRate = -1 * characterState.lightRegenRate;
		}

		// Right/Left trigger is down
		if(Input.GetAxis("ThrowRight") > 0 || Input.GetAxis("ThrowLeft") > 0){
			// Lock the characters movementDirection
			characterState.movementDirectionLocked = true;
		} 
		// All triggers have been released
		else if (characterState.movementDirectionLocked == true){
			// Unlock the characters movement direction
			characterState.movementDirectionLocked = false;
		}

	}
}
