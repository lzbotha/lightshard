using UnityEngine;
using System.Collections;

public class CharacterLightAbilities : MonoBehaviour {

	private CharacterState characterState;

	public GameObject lightShard;
	public Transform cameraPosition;

	public float flashLightRadiusIncrease = 2.0f;
	public float flashLightRegenDebuff = -0.5f;
	public float flashCost = 2.0f;

	private bool wasRightAxisDown;
	private bool wasLeftAxisDown;

	// Use this for initialization
	void Start () {
		characterState = gameObject.GetComponent<CharacterState>();
	}

	bool isAxisDown(string axis) {
		return Input.GetAxis (axis) > 0;
	}

	void updateFlash() {
		// If the player activates flash and there is no current flash active
		if(Input.GetButtonDown("Flash") && characterState.isLightRegenPositive() && characterState.canUseAbility(flashCost)){
			// Set the minimum flash radius to the characters preflash minus flashcost
			characterState.setFlashDeactivationRadius(characterState.getLightRadius() - flashCost);
			// Increase the characters light radius
			characterState.changeLightRadiusBy(flashLightRadiusIncrease);
			// Apply the debuff to the characters light regen rate
			characterState.setLightRegenRate(flashLightRegenDebuff);
		}
	}

	void updateLockMovement() {
		// Lock the character's movement if the Right or Left trigger is down.
		characterState.movementDirectionLocked = isAxisDown("ThrowRight") || isAxisDown("ThrowLeft");
	}

	void throwLightShard() {
		// Calculate the direction to throw the lightshard
		Vector3 throwDirection = cameraPosition.position - this.transform.position;
		throwDirection.y = 0;
		throwDirection.Normalize();
		Vector3 input = new Vector3 (Input.GetAxis ("MovementHorizontal"), 0.0f, Input.GetAxis ("MovementVertical"));
		throwDirection = Quaternion.LookRotation (throwDirection) * input;
		
		GameObject ls = Instantiate(lightShard, transform.position, Quaternion.identity) as GameObject;
		

		// Set the character which cast this LightShard on the LightShardController script
		// this allows the LightShard access to the characterState of the character that 
		// cast it
		LightShardController lsc = ls.GetComponent<LightShardController>();
		lsc.setCharacter(this.gameObject);
		lsc.setKey(characterState.lightShards.addLightShard(ls));

		ls.rigidbody.AddForce(300 * throwDirection);
		ls.rigidbody.AddForce(400 * Vector3.up);
		Physics.IgnoreCollision(ls.collider, GetComponentInChildren<SphereCollider>());

		
	}

	void updateThrowLightShard() {
		// If the player has just released the right axis.
		if (this.wasRightAxisDown && !isAxisDown ("ThrowRight")) {
			throwLightShard();
		}
		if (this.wasLeftAxisDown && !isAxisDown ("ThrowLeft")) {
			throwLightShard();
		}
		this.wasRightAxisDown = isAxisDown("ThrowRight");
		this.wasLeftAxisDown = isAxisDown("ThrowLeft");
	}
	
	// Update is called once per frame
	void Update () {
		// NOTE: do not change the order of these methods else everything will break
		updateFlash ();

		updateThrowLightShard ();
		
		updateLockMovement ();

	}
}
