using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	public Transform cameraPosition;
	public float forceMultiplier;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 movementDirection = cameraPosition.position - this.transform.position;
		movementDirection.y = 0;
		movementDirection.Normalize ();

		Vector3 input = new Vector3 (Input.GetAxis ("MovementHorizontal"), 0.0f, Input.GetAxis ("MovementVertical"));

		if (Vector3.Magnitude(this.rigidbody.velocity) >= 0.1) {
			Vector3 temp = this.transform.forward;
			temp.x = this.rigidbody.velocity.x;
			temp.z = this.rigidbody.velocity.z;
			this.transform.forward = temp;
		}

		this.rigidbody.AddForce(Quaternion.LookRotation (movementDirection) * input * forceMultiplier);


	}
}
