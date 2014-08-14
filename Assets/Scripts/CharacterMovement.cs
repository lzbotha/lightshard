using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	public Transform cameraPosition;
	public float forceMultiplier;
	public float jumpForce;

	// Use this for initialization
	void Start () {
	
	}

	bool IsGrounded() {
		// This method relies on the collider attached to the character, currently a sphere
		// 		I don't want to hard code this yet
		return true;
		// return Physics.Raycast(transform.position, Vector3.down, gameObject.collider.bounds.extents.y + 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		
		// If the player presses the jump button apply the jumping force
		if(Input.GetButtonDown("Jump"))
			this.rigidbody.AddForce(0, jumpForce, 0);
	}

	void FixedUpdate() {
		// Calculate the forward direction under current camera rotation
		Vector3 movementDirection = cameraPosition.position - this.transform.position;
		movementDirection.y = 0;
		movementDirection.Normalize ();

		// Get the input (in world coordinates)
		Vector3 input = new Vector3 (Input.GetAxis ("MovementHorizontal"), 0.0f, Input.GetAxis ("MovementVertical"));

		// Rotate the character to face the direction it is moving in
		if (Vector3.Magnitude(this.rigidbody.velocity) >= 0.1) {
			Vector3 temp = this.transform.forward;
			temp.x = this.rigidbody.velocity.x;
			temp.z = this.rigidbody.velocity.z;
			this.transform.forward = temp;
		}

		// Rotate the input from world space to camera space and apply a force in the appropriate direction
		this.rigidbody.AddForce(Quaternion.LookRotation (movementDirection) * input * forceMultiplier);
	}
}
