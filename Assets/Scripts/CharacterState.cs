using UnityEngine;
using System.Collections;

// TODO: rename this appropriately
public class CharacterState : MonoBehaviour {
	// Class to hold all state of the character that needs to be accessed
	// by multiple scripts or objects
	
	public float lightRadius = 0.0f;
	public float lightRegenRate = 0.5f;
	
	// Can the character currently change direction in the XZ plane
	public bool movementDirectionLocked;

	void Start() {
		movementDirectionLocked = false;
	}

}
