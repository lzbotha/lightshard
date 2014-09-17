using UnityEngine;
using System.Collections;

public class FLockAgentBehaviour : MonoBehaviour {
	public AgentState agentState;
	public AgentMovement agentMovement;

	private enum states {IDLE, CHASE, AVOID, FLEE, MERGE};

	public float cohesionWeight = 1.0f;
	public float seperationWeight = 1.0f;
	public float alignmentWeight = 1.0f;

	private Vector3 alignment = Vector3.zero;
	private Vector3 cohesion = Vector3.zero;
	private Vector3 seperation = Vector3.zero;
	private int neighbourCount = 0;


	void OnTriggerStay(Collider other){
		if (other.tag == "FlockAgent") {

			// alignment
			neighbourCount++;
			alignment += other.gameObject.GetComponent<AgentState>().getVelocity();

			//cohesion
			cohesion += other.transform.position;

			//seperation
			seperation += other.transform.position - this.transform.position;
		}
	}

	private Vector3 calculateFlockVelocity(int neighbourcount){
		alignment /= neighbourCount;
		alignment.Normalize();
		
		cohesion /= neighbourCount;
		cohesion = cohesion - this.transform.position;
		cohesion.Normalize();
		
		seperation /= neighbourCount;
		seperation *= -1;
		seperation.Normalize();

		Vector3 direction = (alignmentWeight * alignment + cohesionWeight * cohesion + seperationWeight * seperation);
		direction.Normalize ();
		return direction;
	}

	void Update(){
		if(this.neighbourCount > 0){
			this.agentMovement.moveInDirection(this.calculateFlockVelocity(neighbourCount));
		}
		this.agentMovement.move ();
	}
}
