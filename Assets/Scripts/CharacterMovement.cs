using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	public float jumpSpeed;
	public float gravity;
	
	public float speed;

	public ThirdPersonCameraController script;

	private float smearTimeRemaining = 0.0f;
	public float smearTime = 0.5f;

	private Vector3 smearStartPosition = Vector3.zero;
	private Vector3 smearEndPosition = Vector3.zero;

	public CharacterState characterState;

	public bool isSmearing() {
		return smearTimeRemaining > 0;
	}

	void advanceSmear(CharacterController controller) {
		float fraction = 1 - smearTimeRemaining / smearTime;
		
		controller.Move(Vector3.Lerp (smearStartPosition, smearEndPosition, fraction) - this.transform.position);
		
		smearTimeRemaining -= Time.deltaTime;
	}

	void startSmear() {
		this.smearTimeRemaining = this.smearTime;

		this.smearStartPosition = this.transform.position;
		this.smearEndPosition = this.transform.position + this.transform.forward * characterState.getLightRadius();
	}

	Vector3 getGravityComponent(CharacterController controller) {
		if (controller.isGrounded) {
			characterState.setVerticalSpeed(0.0f);
			if ( Input.GetButtonDown ("Jump")) {
				characterState.setVerticalSpeed(characterState.getVerticalSpeed() + jumpSpeed);
			}
		} else {
			characterState.setVerticalSpeed(characterState.getVerticalSpeed() - gravity);
		}
		return new Vector3 (0.0f, characterState.getVerticalSpeed (), 0.0f);
	}

	Vector3 getMovementComponent() {
		Vector3 input = new Vector3 (Input.GetAxis ("MovementHorizontal"), 0.0f, Input.GetAxis ("MovementVertical"));
		
		input.Normalize ();
		
		// Calculate the forward direction under current camera rotation
		Vector3 movementDirection = script.cameraTargetLocation - this.transform.position;
		movementDirection.y = 0;
		movementDirection.Normalize ();
		characterState.setCurrentForwardDirection(movementDirection);
		
		return Quaternion.LookRotation(movementDirection) * input * speed;
	}

	void lookInDirectionOfVector(Vector3 vector) {
		if (Vector3.Magnitude(vector) >= 0.3) {
			this.transform.forward = new Vector3(
				vector.x,
				0.0f,
				vector.z
			);
		}
	}

	void Update() {
		CharacterController controller = GetComponent<CharacterController> ();

		if (isSmearing()) {
			gameObject.layer = LayerMask.NameToLayer("SmearingPlayer");
			advanceSmear(controller);
		} else {
			gameObject.layer = LayerMask.NameToLayer("Default");
			if (Input.GetButtonDown("Smear")) {
				startSmear();
			} else {
				Vector3 positionDelta = Vector3.zero;

				positionDelta += this.getGravityComponent(controller);
			
				if(!characterState.isMovementDirectionLocked()){
					positionDelta += this.getMovementComponent();
					lookInDirectionOfVector(positionDelta);
				}

				positionDelta += new Vector3(0.0f, -1.0f, 0.0f);
				
				controller.Move (positionDelta * Time.deltaTime);
			}

		}
	}
}
