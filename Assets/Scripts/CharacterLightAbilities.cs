using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterLightAbilities : MonoBehaviour {
	public CharacterState characterState;

	public GameObject lightShard;
	public Transform cameraPosition;

	public float flashLightRadiusIncrease = 2.0f;
	public float flashLightRegenDebuff = -0.5f;
	public float flashCost = 2.0f;

	private bool wasRightAxisDown;
	private bool wasLeftAxisDown;

	public GameObject lightShardMarker;
	private int hitMarkerLightShardID = -1;
	private List<GameObject> lightShardMarkers = new List<GameObject>();


	bool isAxisDown(string axis) {
		// Return whether the given joystick axis is down.
		return Input.GetAxis (axis) > 0;
	}

	void updateFlash() {
		// If the player activates flash and there is no current flash active.
		if(Input.GetButtonDown("Flash") && characterState.isLightRegenPositive() && characterState.canUseAbility(flashCost)){
			// Set the minimum flash radius to the characters preflash minus flashcost.
			characterState.setFlashDeactivationRadius(characterState.getLightRadius() - flashCost);
			// Increase the characters light radius.
			characterState.changeLightRadiusBy(flashLightRadiusIncrease);
			// Apply the debuff to the characters light regen rate.
			characterState.setLightRegenRate(flashLightRegenDebuff);
		}
	}

	void updateLockCamera() {
		// Lock the character's movement if the Right or Left trigger is down.
		// characterState.setMovementDirectionLocked(isAxisDown("ThrowRight") || isAxisDown("ThrowLeft"));
		characterState.setCameraDirectionLocked(isAxisDown("ThrowRight") || isAxisDown("ThrowLeft"));
	}

	void throwLightShard() {
		// Calculate the direction to throw the lightshard
		Vector3 throwDirection = cameraPosition.position - this.transform.position;
		throwDirection.y = 0;
		throwDirection.Normalize();
		Vector3 input = new Vector3 (Input.GetAxis ("CameraHorizontal"), 0.0f, -Input.GetAxis ("CameraVertical"));
		throwDirection = Quaternion.LookRotation (throwDirection) * input;
		
		GameObject ls = Instantiate(lightShard, transform.position, Quaternion.identity) as GameObject;
		

		// Set the character which cast this LightShard on the LightShardController script
		// this allows the LightShard access to the characterState of the character that 
		// cast it
		LightShardController lsc = ls.GetComponent<LightShardController>();
		lsc.setCharacter(this.gameObject);

		// Don't change the order of this or bad things will happen
		int key = characterState.lightShards.addLightShard (ls);
		lsc.setKey(key);

		this.characterState.latestLightShardID = key;

		lsc.getThrown(this.transform.position, throwDirection);
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

	void handleTeleport(string button){
		if(Input.GetButton(button)) {
			characterState.setCameraDirectionLocked(true);

			if(characterState.lightShards.getNumberOfLightShards() > 0){
				List<KeyValuePair<int, Vector3>> directionsToLightShards = characterState.lightShards.getDirectionsToLightShardsFromPosition(this.transform.position);
				drawMarkers(directionsToLightShards);

				// Do raycasting stuff here (must be after drawing the markers)
				Vector3 input = new Vector3 (Input.GetAxis ("CameraHorizontal"), 0.0f, -Input.GetAxis ("CameraVertical"));


				input = Quaternion.LookRotation(characterState.getCurrentForwardDirection()) * input;
				
				input.Normalize();

				if (input.magnitude <= 0.8) {
					hitMarkerLightShardID = this.characterState.latestLightShardID;
				} else {
					hitMarkerLightShardID = -1;
					float smallestDifference = 2;
					foreach(KeyValuePair<int, Vector3> direction in directionsToLightShards) {
						float difference = (direction.Value - input).magnitude;
						if (difference <= smallestDifference) {
							smallestDifference = difference;
							hitMarkerLightShardID = direction.Key;
						}
					}
				}
			}
		}
		else if (Input.GetButtonUp(button)) {
			foreach(GameObject marker in lightShardMarkers){
				Destroy(marker);
			}
			characterState.setCameraDirectionLocked(false);

			// Teleport to a marker if one is selected
			if(hitMarkerLightShardID != -1) {
				GameObject hitLightShard = characterState.lightShards.getLightShard(hitMarkerLightShardID);
				characterState.setVerticalSpeed(0.0f);
				this.transform.position = hitLightShard.transform.position + new Vector3(0, 1.0f, 0);
				// Destroy the lightshard
				// hitLightShard.GetComponent<LightShardController>().cleanUp();
			}
		}

	}

	void drawMarkers(List<KeyValuePair<int, Vector3>> directionsToLightShards){
		foreach(GameObject marker in lightShardMarkers){
			Destroy(marker);
		}
		lightShardMarkers.Clear();
		foreach(KeyValuePair<int, Vector3> shardDirection in directionsToLightShards){
			GameObject marker = Instantiate(lightShardMarker, transform.position + shardDirection.Value, Quaternion.identity) as GameObject;
			marker.GetComponent<LightShardMarker>().lightShardID = shardDirection.Key;
			marker.transform.forward = shardDirection.Value;
         	lightShardMarkers.Add(marker);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// NOTE: do not change the order of these methods else everything will break
		updateFlash();

		updateThrowLightShard();
		
		updateLockCamera();

		handleTeleport("TeleportRight");
		handleTeleport("TeleportLeft");
	}
}
