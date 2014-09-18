using UnityEngine;
using System.Collections;

public class CharacterMovement : BasicMovement {

	public ThirdPersonCameraController script;

	private float smearTimeRemaining = 0.0f;
	public float smearTime = 0.5f;

	private Vector3 smearStartPosition = Vector3.zero;
	private Vector3 smearEndPosition = Vector3.zero;

	public CharacterState characterState;

	private Vector3 moveComponent = Vector3.zero;

	public float minYPosition = -50.0f;

	public bool isSmearing() {
		return smearTimeRemaining > 0;
	}

	void updateSmear() {
		float fraction = 1 - smearTimeRemaining / smearTime;
		
		this.controller.Move(Vector3.Lerp (smearStartPosition, smearEndPosition, fraction) - this.transform.position);
		
		smearTimeRemaining -= Time.deltaTime;
	}

	void startSmear() {
		this.smearTimeRemaining = this.smearTime;

		this.smearStartPosition = this.transform.position;
		this.smearEndPosition = this.transform.position + this.transform.forward * characterState.getLightRadius();
	}

	void updateGravity(){
		if (this.controller.isGrounded && characterState.getVelocity().y <= 0.0f) {
			characterState.setVelocityY(0.0f);
			if (Input.GetButtonDown (characterState.getPlayerTag() + "Jump")) {
				characterState.setVelocityY(characterState.getVelocity().y + jumpSpeed);
			}
		} else {
			characterState.setVelocityY(characterState.getVelocity().y + this.gravity * Time.deltaTime);
		}
		// Gravity fudge factor
		characterState.setVelocity(characterState.getVelocity() + new Vector3(0.0f, -1.0f, 0.0f));
	}

	Vector3 getMovementComponent() {
		Vector3 input = new Vector3 (Input.GetAxis (characterState.getPlayerTag() + "MovementHorizontal"), 0.0f, Input.GetAxis (characterState.getPlayerTag() + "MovementVertical"));
		
		input.Normalize ();
		
		// Calculate the forward direction under current camera rotation
		Vector3 movementDirection = script.cameraTargetLocation - this.transform.position;
		movementDirection.y = 0;
		movementDirection.Normalize ();
		characterState.setCurrentForwardDirection(movementDirection);
		
		return Quaternion.LookRotation(movementDirection) * input * speed;
	}

	void lookInDirectionOfVector(Vector3 vector) {
		if (Vector3.Magnitude(new Vector3(vector.x, 0.0f, vector.z)) >= 0.3) {
			this.transform.forward = new Vector3(
				vector.x,
				0.0f,
				vector.z
			);
		}
	}

	void Update() {
		if (isSmearing()) {
			gameObject.layer = LayerMask.NameToLayer("SmearingPlayer");
			updateSmear();
		} else {
			gameObject.layer = LayerMask.NameToLayer("Default");

			if(this.transform.position.y <= minYPosition){
				this.characterState.respawn();
			}

			if (Input.GetButtonDown(characterState.getPlayerTag() + "Smear")) {
				startSmear();
			} else {

				updateGravity();
			
				if(!characterState.isMovementDirectionLocked()){
					// Remove movement component from previous update (this must be done so that movement is instantateous)
					this.characterState.setVelocity(characterState.getVelocity() - moveComponent);
					this.moveComponent = this.getMovementComponent();
					if(this.controller.isGrounded)
						applyFriction();
					// Update the velocity component to reflect character movement
					this.characterState.setVelocity(characterState.getVelocity() + moveComponent);
					lookInDirectionOfVector(characterState.getVelocity());
				}

				this.controller.Move (characterState.getVelocity() * Time.deltaTime);
			}

		}
	}
}
