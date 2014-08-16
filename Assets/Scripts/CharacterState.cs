using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterState : MonoBehaviour {
	// Class to hold all state of the character that needs to be accessed
	// by multiple scripts or objects
	
	// These values are determined elsewhere and set in the Start() method
	// Changing them here will have no effect

	public bool inAir = false;
	public float lightRadius;
	public float lightRegenRate;	
	public float flashDeactivationRadius;

	// This value must be set here
	public float minLightRadius = 2.0f;
	
	// Can the character currently change direction in the XZ plane
	public bool movementDirectionLocked;

	public CharacterLightShardContainer lightShards = new CharacterLightShardContainer(10);

	public float getLightRadius(){
		return lightRadius;
	}

	public float getLightRegenRate(){
		return lightRegenRate;
	}

	public bool isLightRegenPositive() {
		return lightRegenRate > 0;
	}

	public void setLightRegenRate(float regen){
		lightRegenRate = regen;
	}

	public void changeLightRadiusBy(float amount){
		lightRadius += amount;
	}

	public bool canUseAbility(float cost) {
		return lightRadius - cost >= minLightRadius;
	}

	void Start() {
		movementDirectionLocked = false;
		flashDeactivationRadius = 0;
	}

}
