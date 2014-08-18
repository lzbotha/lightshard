using UnityEngine;
using System.Collections;

public class CharacterMovement2 : MonoBehaviour {
	public Vector3 velocity;
	public float jumpSpeed;
	public float gravity;
	
	public float speed;

	public ThirdPersonCameraController script;

	void Update() {
		CharacterController controller = GetComponent<CharacterController> ();

		if (controller.isGrounded) {
			Debug.Log ("Grounded.");
			velocity.y = 0.0f;
			if ( Input.GetButtonDown ("Jump")) {
				velocity.y += jumpSpeed;
			}
		} else {
			Debug.Log ("NOT GROUNDED.");
			velocity.y -= gravity;
		}

		Vector3 input = new Vector3 (Input.GetAxis ("MovementHorizontal"), 0.0f, Input.GetAxis ("MovementVertical"));

		input.Normalize ();

		// Calculate the forward direction under current camera rotation
		Vector3 movementDirection = script.cameraTargetLocation - this.transform.position;
		movementDirection.y = 0;
		movementDirection.Normalize ();


		var moveVector = Quaternion.LookRotation(movementDirection) * input * speed;

		if (Vector3.Magnitude(moveVector) >= 0.1) {
			Vector3 temp = this.transform.forward;
			temp.x = moveVector.x;
			temp.z = moveVector.z;
			this.transform.forward = temp;
		}

		controller.Move (Time.deltaTime * (
			moveVector +
			// Stop from bouncing off floor constantly.
			new Vector3 (0.0f, -0.01f, 0.0f) +
			velocity
		));
	}

}
