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

	public int playerChaseThreshold = 3;
	public float playerLightRunThreshold = 5.5f;

	private Vector3 alignment = Vector3.zero;
	private Vector3 cohesion = Vector3.zero;
	private Vector3 seperation = Vector3.zero;

	private HashSet<GameObject> neighbours = new HashSet<GameObject> ();
	private GameObject player;
	private bool playerNear = false;

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			player = other.gameObject;
			playerNear = true;
		}
		else if (other.tag == "FlockAgent" && other.gameObject != this.gameObject && other.isTrigger == false) {
			neighbours.Add (other.gameObject);
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			playerNear = false;
		}
		else if (other.tag == "FlockAgent" && other.gameObject != this.gameObject && other.isTrigger == false) {
			neighbours.Remove (other.gameObject);
		}
	}

	// Calculates the direction of this agent based on flocking rules (only)
	private Vector3 calculateFlockDirectionComponent(){
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

			this.alignment = Vector3.zero;
			this.cohesion = Vector3.zero;
			this.seperation = Vector3.zero;

			return direction;
		}

		//if this flock is on its own chose a random direction
		Vector2 rand = Random.insideUnitCircle; // <-- this is a bit shite

		return new Vector3 (rand.x, 0.0f, rand.y);
	}

	private Vector3 calculatePlayerDirectionComponent(){
		if (this.playerNear) {

			if(neighbours.Count >= this.playerChaseThreshold && this.player.GetComponent<CharacterState>().getLightRadius() < this.playerLightRunThreshold){
				return player.transform.position - this.transform.position;
			} else {
				return this.transform.position - player.transform.position;
			}
		}
		return Vector3.zero;
	}

	void Update(){
		Vector3 direction = this.calculateFlockDirectionComponent () + this.calculatePlayerDirectionComponent ();
		direction.Normalize ();
		this.agentMovement.moveInDirection(direction);
		this.agentMovement.move ();
	}
}
