using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	public Transform cameraPosition;
	public float forceMultiplier;
	public float jumpForce;

	private Vector3 movementDirection;
	private Vector3 input;
	private CharacterState characterState;
	private float normalGravityY = -9.8f;
	private float heavyGravityY = -70f;
	private Vector3 normalGravity;
	private Vector3 heavyGravity;
	private GameObject smearChecker;

	// Use this for initialization
	void Start () {
		characterState = gameObject.GetComponent<CharacterState>();
		normalGravity = new Vector3 (0, normalGravityY, 0);
		heavyGravity = new Vector3 (0, heavyGravityY, 0);
		smearChecker = transform.Find ("SmearChecker").gameObject;
	}

	// Update is called once per frame
	void Update () {

		if (characterState.inAir && this.rigidbody.velocity.y <=0 && Physics.gravity.y > heavyGravityY && this.transform.position.y > 1f) {
			Debug.Log("gravity = "+Physics.gravity.y);
			Physics.gravity = Vector3.Lerp(Physics.gravity, heavyGravity, 1f);
			this.rigidbody.drag = 0;
		}else if(characterState.inAir && this.rigidbody.velocity.y <=0 && Physics.gravity.y < normalGravityY && this.transform.position.y < 1f){
			Debug.Log("gravity2 = "+Physics.gravity.y);
			Physics.gravity = Vector3.Lerp(Physics.gravity, normalGravity, 5f);
		}

		// If the player presses the jump button apply the jumping force
		if(Input.GetButtonDown("Jump") && !characterState.inAir){
			this.rigidbody.AddForce(0, jumpForce, 0);
			smearChecker.transform.localPosition = new Vector3(0,0,0);
		}

		if(Input.GetButtonDown("Smear")){
//			this.rigidbody.AddForce(Quaternion.LookRotation (movementDirection) * input * 300);
			Vector3 p1 = transform.position + transform.forward*characterState.lightRadius;
			Debug.DrawLine(transform.position, p1, Color.cyan);
		}
	}

	void FixedUpdate() {
		// Calculate the forward direction under current camera rotation
		movementDirection = cameraPosition.position - this.transform.position;
		movementDirection.y = 0;
		movementDirection.Normalize ();

		
		// If the characters movementDirection is not locked
		if(characterState.movementDirectionLocked == false){
			// Get the input (in world coordinates)
			//Debug.Log("input axis="+Input.GetAxis ("MovementHorizontal"));
			input = new Vector3 (Input.GetAxis ("MovementHorizontal"), 0.0f, Input.GetAxis ("MovementVertical"));

			// Rotate the character to face the direction it is moving in
			if (Vector3.Magnitude(this.rigidbody.velocity) >= 0.1) {
				Vector3 temp = this.transform.forward;
				temp.x = this.rigidbody.velocity.x;
				temp.z = this.rigidbody.velocity.z;
				this.transform.forward = temp;
			}

		}

		// Rotate the input from world space to camera space and apply a force in the appropriate direction
		this.rigidbody.AddForce(Quaternion.LookRotation (movementDirection) * input * forceMultiplier);

		// TODO: add calulation so that the player smears to the edge of their light radius
//		if(Input.GetButtonDown("Smear")){
//			this.rigidbody.AddForce(Quaternion.LookRotation (movementDirection) * input * 300);
//		}
	}

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "Floor"){
			Debug.Log("Touching floor");
			characterState.inAir = false;
			this.rigidbody.mass = 60;
			this.rigidbody.drag = 5;
			Physics.gravity = normalGravity;
		}
	}

	void OnCollisionExit(Collision collision){
		if(collision.gameObject.tag == "Floor"){
			Debug.Log("Not Touching floor");
			characterState.inAir = true;
			//this.rigidbody.mass = 0.01f;
		}
	}
}
