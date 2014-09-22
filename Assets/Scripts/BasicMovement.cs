using UnityEngine;
using System.Collections;

public class BasicMovement : MonoBehaviour {
	
	public float gravity;
	public float jumpSpeed;
	public float speed;
	public float mass = 1.0f;

	public float friction;

	private BasicState associatedState;
	protected CharacterController controller;

	void Start(){
		this.associatedState = this.GetComponent<BasicState>();
		this.controller = this.GetComponent<CharacterController> ();
	}

	public void applyFriction(){
		float friction = this.friction * Time.deltaTime;
		if(this.associatedState.getVelocity().x - friction > 0)
			this.associatedState.setVelocityX(this.associatedState.getVelocityX() - friction);
		else if(this.associatedState.getVelocity().x + friction < 0)
			this.associatedState.setVelocityX(this.associatedState.getVelocityX() + friction);
		else
			this.associatedState.setVelocityX(0);

		if(this.associatedState.getVelocity().z - friction > 0)
			this.associatedState.setVelocityZ(this.associatedState.getVelocityZ() - friction);
		else if(this.associatedState.getVelocity().z + friction < 0)
			this.associatedState.setVelocityZ(this.associatedState.getVelocityZ() + friction);		
		else
			this.associatedState.setVelocityZ(0);
	}

	public void applyForce(Vector3 force){
		this.associatedState.setVelocity(associatedState.getVelocity() + (1/this.mass) * force);
	}

}
