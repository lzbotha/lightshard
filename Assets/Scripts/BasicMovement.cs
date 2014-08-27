using UnityEngine;
using System.Collections;

public class BasicMovement : MonoBehaviour {
	
	public float gravity;
	public float jumpSpeed;
	public float speed;
	public float mass = 1.0f;

	public float friction;

	protected CharacterController controller;

	private BasicState associatedState;

	void Start(){
		this.associatedState = this.GetComponent<BasicState>();
		this.controller = GetComponent<CharacterController> ();
	}

	public void applyFriction(){
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
		associatedState.setVelocity(associatedState.getVelocity() + (1/this.mass) * force);
	}

}
