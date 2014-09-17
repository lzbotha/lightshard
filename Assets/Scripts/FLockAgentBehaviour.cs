using UnityEngine;
using System.Collections;
using System.Collections.Generic;
	
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

	private HashSet<GameObject> neighbours = new HashSet<GameObject> ();

	void OnTriggerEnter(Collider other){

		if (other.tag == "FlockAgent" && other.gameObject != this.gameObject && other.isTrigger == false) {
			neighbours.Add (other.gameObject);
		}
	}

	void OnTriggerExit(Collider other){

		if (other.tag == "FlockAgent" && other.gameObject != this.gameObject && other.isTrigger == false) {
			neighbours.Remove (other.gameObject);
		}
	}

	private Vector3 calculateFlockVelocity(){
		if (this.neighbours.Count > 0) {
			foreach (GameObject obj in neighbours) {
				// alignment
				alignment += obj.GetComponent<AgentState> ().getVelocity ();
			
				//cohesion
				cohesion += obj.transform.position;
			
				//seperation
				seperation += obj.transform.position - this.transform.position;
			}


			alignment /= this.neighbours.Count;
			alignment.Normalize ();

			cohesion /= this.neighbours.Count;
			cohesion = cohesion - this.transform.position;
			cohesion.Normalize ();

			seperation /= this.neighbours.Count;
			seperation *= -1;
			seperation.Normalize ();

			Vector3 direction = (alignmentWeight * alignment + cohesionWeight * cohesion + seperationWeight * seperation);
			direction.Normalize ();

			this.alignment = Vector3.zero;
			this.cohesion = Vector3.zero;
			this.seperation = Vector3.zero;

			return direction;
		}
		return Vector3.zero;
	}

	void Update(){
		this.agentMovement.moveInDirection(this.calculateFlockVelocity());
		this.agentMovement.move ();
	}
}
