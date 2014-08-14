using UnityEngine;
using System.Collections;

// TODO: rename this appropriately
public class CharacterState : MonoBehaviour {
	// Class to hold all state of the character that needs to be accessed
	// by multiple scripts or objects
	
	// These values are determined elsewhere and set in the Start() method
	public float lightRadius;
	public float lightRegenRate;	
	public float flashMinimumRadius;

	// This value must be set here
	public float minLightRadius = 2.0f;
	
	// Can the character currently change direction in the XZ plane
	public bool movementDirectionLocked;

	void Start() {
		movementDirectionLocked = false;
	}

}
