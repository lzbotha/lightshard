using UnityEngine;
using System.Collections;

public class CharacterMovement2 : MonoBehaviour {
	public Vector3 velocity;
	public float jumpSpeed;
	public float gravity;
	
	public float speed;
	private Vector3 moveVector = Vector3.zero;

	public ThirdPersonCameraController script;

	private float smearTimeRemaining = 0.0f;
	public float smearTime = 0.5f;

	private Vector3 smearStartPosition = Vector3.zero;
	private Vector3 smearEndPosition = Vector3.zero;

	public CharacterState characterState;

	void Update() {
		CharacterController controller = GetComponent<CharacterController> ();

		if (smearTimeRemaining > 0) {
			float fraction = 1 - smearTimeRemaining / smearTime;

			controller.Move(Vector3.Lerp (smearStartPosition, smearEndPosition, fraction) - this.transform.position);

			smearTimeRemaining -= Time.deltaTime;
		} else {

			if (Input.GetButtonDown("Smear")) {
				this.smearTimeRemaining = this.smearTime;
				this.smearStartPosition = this.transform.position;
				this.smearEndPosition = this.transform.position + this.transform.forward * characterState.getLightRadius();
			}

			if (controller.isGrounded) {
				velocity.y = 0.0f;
				if ( Input.GetButtonDown ("Jump")) {
					velocity.y += jumpSpeed;
				}
			} else {
				velocity.y -= gravity;
			}



			if(!characterState.isMovementDirectionLocked()){
				Vector3 input = new Vector3 (Input.GetAxis ("MovementHorizontal"), 0.0f, Input.GetAxis ("MovementVertical"));

				input.Normalize ();

				// Calculate the forward direction under current camera rotation
				Vector3 movementDirection = script.cameraTargetLocation - this.transform.position;
				movementDirection.y = 0;
				movementDirection.Normalize ();
				characterState.setCurrentForwardDirection(movementDirection);


				moveVector = Quaternion.LookRotation(movementDirection) * input * speed;

				if (Vector3.Magnitude(moveVector) >= 0.1) {
					Vector3 temp = this.transform.forward;
					temp.x = moveVector.x;
					temp.z = moveVector.z;
					this.transform.forward = temp;
				}
			}

			controller.Move (Time.deltaTime * (
				moveVector +
				// Stop from bouncing off floor constantly.
				new Vector3 (0.0f, -0.01f, 0.0f) +
				velocity
			));
		}
	}
}
