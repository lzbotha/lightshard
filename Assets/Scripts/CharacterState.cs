using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterState : MonoBehaviour {
	// Class to hold all state of the character that needs to be accessed
	// by multiple scripts or objects
	
	// These values are determined elsewhere and set in the Start() method
	// Changing them here will have no effect
	public float lightRadius;
	public float lightRegenRate;	
	public float flashMinimumRadius;

	// This value must be set here
	public float minLightRadius = 2.0f;
	
	// Can the character currently change direction in the XZ plane
	public bool movementDirectionLocked;

	public Dictionary<string, GameObject> lightshards;

	void Start() {
		movementDirectionLocked = false;
	}

}
