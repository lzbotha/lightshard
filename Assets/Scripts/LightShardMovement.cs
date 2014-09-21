using UnityEngine;
using System.Collections;

public class LightShardMovement : BasicMovement {

	public float throwDistance = 3.0f;
	public float throwTime = 1.5f;
	private Vector3 throwVelocity = Vector3.zero;
	public LightShardState state;


	public void throwLightShard(Vector3 position, Vector3 direction){
		this.transform.position = position;
		
		// Calculate vertical velocity
		//throwVelocity.y = this.arcHeight/(this.throwTime * 0.5f) - 0.5f * this.gravity * (this.throwTime * 0.5f);
		throwVelocity.y = -this.gravity * 0.5f * throwTime;

		// calculate the horizontal components
		this.speed = throwDistance/throwTime;
		throwVelocity.x = this.speed * direction.x;
		throwVelocity.z = this.speed * direction.z;

		state.setVelocity(throwVelocity);
		print (state.getVelocityY ());
	}
	

	void FixedUpdate () {
		CharacterController controller = GetComponent<CharacterController> ();
		controller.Move (Time.deltaTime * (
			// Stop from bouncing off floor constantly.
			new Vector3 (0.0f, -0.01f, 0.0f) +
			state.getVelocity()
		));

		state.setVelocityY(state.getVelocityY() + Time.deltaTime * this.gravity);

		if (controller.isGrounded && state.getVelocityY() <= 0) {
			this.state.setVelocityY(0.0f);
			this.applyFriction();
			this.state.setLanded(true);
		}
	}
}
