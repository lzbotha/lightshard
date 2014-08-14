using UnityEngine;
using System.Collections;

// TODO: rename this appropriately
public class CharacterState : MonoBehaviour {
	// Class to hold all state of the character that needs to be accessed
	// by multiple scripts or objects

	public bool inAir = false;
	public float lightRadius;

	// Can the character currently move in the XZ plane
	public bool movementDirectionLocked;

	void Start() {
		movementDirectionLocked = false;
	}
}
