using UnityEngine;
using System.Collections;

public class AgentMovement : BasicMovement {
	public AgentState agentState;

	private Vector3 movementComponent = Vector3.zero;

	public void chaseTarget(Vector3 targetPosition){
		this.agentState.findPathTo (targetPosition);

		// remove previous iterations movement component
		agentState.setVelocity(agentState.getVelocity() - this.movementComponent);

		// if the agent is on the ground apply friction to non movementComponent velocities
		//if (this.controller.isGrounded)
			//applyFriction ();
		

		// calculate new movement component
		Vector3 direction = (this.agentState.getNextPathSegment () - this.transform.position);
		direction.Normalize();

		movementComponent = direction * this.speed;
		print (movementComponent);

		this.agentState.setVelocity (this.agentState.getVelocity () + movementComponent);
	}

	public void moveInDirection(Vector3 direction){
		this.chaseTarget (this.transform.position + direction * this.speed);
	}

	public void move(){
		this.controller.Move (agentState.getVelocity() * Time.deltaTime);
	}
}
