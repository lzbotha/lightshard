﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterLightAbilities : MonoBehaviour {
	private CharacterState characterState;

	public GameObject lightShard;
	public Transform cameraPosition;

	public float flashLightRadiusIncrease = 2.0f;
	public float flashLightRegenDebuff = -0.5f;
	public float flashCost = 2.0f;

	private bool wasRightAxisDown;
	private bool wasLeftAxisDown;

	private bool shouldDrawTeleportOptions = false;
	private List<KeyValuePair<int, Vector3>> directionsToLightShards = new List<KeyValuePair<int, Vector3>>();
	

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
		characterState.setMovementDirectionLocked(isAxisDown("ThrowRight") || isAxisDown("ThrowLeft"));
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

		ls.rigidbody.AddForce(3000 * throwDirection);
		ls.rigidbody.AddForce(400 * Vector3.up);
		// There is currently no csphere collider on the cahracter
		// Physics.IgnoreCollision(ls.GetComponent<CapsuleCollider>(), this.GetComponentInChildren<SphereCollider>());	
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
			shouldDrawTeleportOptions = true;
			
			// Direction from the player to the camera
			Vector3 directionToCamera = cameraPosition.position - this.transform.position;
			directionToCamera.y = 0;
			directionToCamera.Normalize();

			if(characterState.lightShards.getNumberOfLightShards() > 0){
				directionsToLightShards = characterState.lightShards.getDirectionsToLightShards(directionToCamera, this.transform.position);
			}
		}
		else if (Input.GetButtonUp(button)) {
			characterState.setCameraDirectionLocked(false);
			shouldDrawTeleportOptions = false;
		}

	}

	void OnGUI() {
		if(shouldDrawTeleportOptions && characterState.lightShards.getNumberOfLightShards() > 0){
			foreach(KeyValuePair<int, Vector3> shardDirection in directionsToLightShards){
				GUI.Box(new Rect(Screen.width/2 + shardDirection.Value.x * 100, Screen.height/2 - shardDirection.Value.z * 100, 10, 10), "X");
			}		
		}
	}
	
	// Update is called once per frame
	void Update () {
		// NOTE: do not change the order of these methods else everything will break
		updateFlash ();

		updateThrowLightShard ();
		
		updateLockMovement ();

		handleTeleport("TeleportRight");
		handleTeleport("TeleportLeft");
	}
}
