using UnityEngine;
using System.Collections;

public class AgentMovement : BasicMovement {
	public AgentState agentState;
	public NavMeshAgent navmesh;

	private Vector3 target;
	private Vector3 movementComponent = Vector3.zero;


	void Start(){
		this.navmesh.speed = this.speed;
		this.navmesh.acceleration = 2 * this.speed;
		this.navmesh.angularSpeed = 360;
	}

	public void chaseTarget(Vector3 targetPosition){
		this.target = targetPosition;
	}

	public void moveInDirection(Vector3 direction){
		this.chaseTarget (this.transform.position + direction * this.speed);
	}

	public void move(){
		this.navmesh.SetDestination(this.target);		
	}
}

