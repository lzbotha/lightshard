using UnityEngine;
using System.Collections;

public class CharacterLightController : MonoBehaviour {

	public CharacterState characterState;

	public float normalLightRadius = 5.0f;
	public float minLightRadius = 2.0f;
	public float normalLightRegenRate = 0.5f;

	// Use this for initialization
	void Start () {
		// RenderSettings.ambientLight = Color.black;
		characterState.setLightRegenRate(normalLightRegenRate);
		characterState.setLightRadius(normalLightRadius);
		characterState.setMinLightRadius(minLightRadius);
	}

	public void disableFlash() {
		characterState.setLightRegenRate(normalLightRegenRate);
		characterState.setFlashDeactivationRadius(0);
	}

	public void restoreLightRadius() {
		this.characterState.setLightRadius(this.normalLightRadius);
	}
	
	void Update () {
		// If a Flash is in progress
		// Both a check for negative regen and a non zero flashDeactivationRadius are necessary
		// incase the player has a negative light regen rate because of level stuff
		if(characterState.isLightRegenNegative() && characterState.getFlashDeactivationRadius() > 0){
			// Decrease light radius
			characterState.changeLightRadiusBy(characterState.getLightRegenRate() * Time.deltaTime);
			// If the flash duration is over, or the character is at minimum light 
			// radius, restore the characters light regen to normal
			if(characterState.getLightRadius() <= characterState.getFlashDeactivationRadius()){
				this.disableFlash();
			}
		}
		// If no flash is in progress and the character has a light regen
		// NOTE: there could be areas in a level where there is no light regen so this
		// 			can't be in an else
		else if (characterState.isLightRegenPositive()) {
			// Restore the characters light radius up to the normal value
			characterState.setLightRadius(Mathf.Clamp(characterState.getLightRadius() + characterState.getLightRegenRate() * Time.deltaTime, characterState.getMinLightRadius(), normalLightRadius));
		}
		gameObject.light.range = characterState.getLightRadius();
	}
}
