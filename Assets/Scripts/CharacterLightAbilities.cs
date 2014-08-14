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
		// Right trigger is down
		if(Input.GetAxis("ThrowRight") > 0){
			// Disable player movement
			characterState.canMove = false;
		} 
		// Right trigger has been released
		else if (characterState.canMove == false){
			characterState.canMove = true;
		}

	}
}
