using UnityEngine;
using System.Collections;

public class CharacterLightController : MonoBehaviour {

	private CharacterState characterState;

	public float normalLightRadius = 5.0f;
	public float normalLightRegenRate = 0.5f;

	// Use this for initialization
	void Start () {
		characterState = this.transform.parent.GetComponent<CharacterState>();
		characterState.lightRegenRate = normalLightRegenRate;
		characterState.lightRadius = normalLightRadius;
	}
	
	void Update () {
		// If a Flash is in progress
		// Both a check for negative regen and a non zero flashDeactivationRadius are necessary
		// incase the player has a negative light regen rate because of level stuff
		if(!characterState.isLightRegenPositive() && characterState.flashDeactivationRadius > 0){
			// Decrease light radius
			characterState.lightRadius += characterState.lightRegenRate * Time.deltaTime;
			// If the flash duration is over, or the character is at minimum light 
			// radius, restore the characters light regen to normal
			if(characterState.lightRadius <= characterState.flashDeactivationRadius){
				characterState.setLightRegenRate(normalLightRegenRate);
				characterState.flashDeactivationRadius = 0;
			}
		}
		// If no flash is in progress and the character has a light regen
		// NOTE: there could be areas in a level where there is no light regen so this
		// 			can't be in an else
		else if (characterState.isLightRegenPositive()) {
			// Restore the characters light radius up to the normal value
			characterState.lightRadius = Mathf.Clamp(characterState.lightRadius + characterState.lightRegenRate * Time.deltaTime, characterState.minLightRadius, normalLightRadius);
		}
		gameObject.light.range = characterState.lightRadius;
	}
}
