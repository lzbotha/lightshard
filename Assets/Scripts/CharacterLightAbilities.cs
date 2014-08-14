using UnityEngine;
using System.Collections;

public class CharacterLightAbilities : MonoBehaviour {

	private CharacterState characterState;

	public float flashLightRadiusIncrease = 2.0f;
	public float flashLightRegenDebuff = -0.5f;
	public float flashCost = 2.0f;

	// Use this for initialization
	void Start () {
		characterState = gameObject.GetComponent<CharacterState>();
	}
	
	// Update is called once per frame
	void Update () {
		// If the player activates flash and there is no current flash active
		if(Input.GetButtonDown("Flash") && characterState.lightRegenRate > 0 && characterState.lightRadius - flashCost >= characterState.minLightRadius){
			// Increase the characters light radius
			characterState.lightRadius += flashLightRadiusIncrease;
			// Apply the debuff to the characters light regen rate
			characterState.lightRegenRate = flashLightRegenDebuff;

			characterState.flashMinimumRadius = characterState.lightRadius - flashCost;
		}

		// Right/Left trigger is down
		if(Input.GetAxis("ThrowRight") > 0 || Input.GetAxis("ThrowLeft") > 0){
			// Lock the characters movementDirection
			characterState.movementDirectionLocked = true;
		} 
		// All triggers have been released and movement is still locked
		else if (characterState.movementDirectionLocked == true){
			// Unlock the characters movement direction
			characterState.movementDirectionLocked = false;
		}

	}
}
