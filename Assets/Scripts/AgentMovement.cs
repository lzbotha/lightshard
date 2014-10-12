using UnityEngine;
using System.Collections;

public class AgentMovement : BasicMovement {
	public AgentState agentState;
	public NavMeshAgent agent;

	private Vector3 target;

	private Vector3 movementComponent = Vector3.zero;

	public void chaseTarget(Vector3 targetPosition){
		this.target = targetPosition;
		//agent.SetDestination(transform.position);
	}

	public void moveInDirection(Vector3 direction){
		this.chaseTarget (this.transform.position + direction * this.speed);
	}

	public void move(){
		//this.controller.Move (this.agentState.getVelocity() * Time.deltaTime);

		this.agent.SetDestination(this.target);

		//if(this.agentState.getVelocity() != Vector3.zero)
		//	this.transform.forward = this.agentState.getVelocity ();
	}
}

