using UnityEngine;
using System.Collections;

public class CharacterLightAbilities : MonoBehaviour {

	private CharacterState characterState;

	public GameObject lightShard;
	public Transform cameraPosition;

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
			// Set the minimum flash radius to the characters preflash minus flashcost
			characterState.flashMinimumRadius = characterState.lightRadius - flashCost;
			// Increase the characters light radius
			characterState.lightRadius += flashLightRadiusIncrease;
			// Apply the debuff to the characters light regen rate
			characterState.lightRegenRate = flashLightRegenDebuff;
		}

		// Right/Left trigger is down
		if(Input.GetAxis("ThrowRight") > 0 || Input.GetAxis("ThrowLeft") > 0){
			// Lock the characters movementDirection
			characterState.movementDirectionLocked = true;
		} 
		// All triggers have been released and movement is still locked
		else if (characterState.movementDirectionLocked == true){
			// Calculate the direction to throw the lightshard
			Vector3 throwDirection = cameraPosition.position - this.transform.position;
			throwDirection.y = 0;
			throwDirection.Normalize();
			Vector3 input = new Vector3 (Input.GetAxis ("MovementHorizontal"), 0.0f, Input.GetAxis ("MovementVertical"));
			throwDirection = Quaternion.LookRotation (throwDirection) * input;

			// TODO: add this to an array of lightShard objects
			GameObject ls = Instantiate(lightShard, transform.position, Quaternion.identity) as GameObject;
			ls.rigidbody.AddForce(300 * throwDirection);
			ls.rigidbody.AddForce(400 * Vector3.up);
			Physics.IgnoreCollision(ls.collider, GetComponentInChildren<SphereCollider>());

			// Unlock the characters movement direction
			characterState.movementDirectionLocked = false;
		}

	}
}
