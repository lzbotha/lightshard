using UnityEngine;
using System.Collections;
using System.Collections.Generic;
	
public class FLockAgentBehaviour : MonoBehaviour {
	public AgentState agentState;
	public AgentMovement agentMovement;

	public float cohesionWeight = 1.0f;
	public float seperationWeight = 1.0f;
	public float alignmentWeight = 1.0f;

	public float seperationThreshold = 2.0f;
	public float attackRange = 0.3f;

	public int playerChaseThreshold;
	public float playerLightRunThreshold = 5.5f;

	private HashSet<GameObject> neighbours = new HashSet<GameObject> ();
	private HashSet<GameObject> players = new HashSet<GameObject> ();

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			players.Add(other.gameObject);
		}
		else if (other.tag == "FlockAgent" && other.gameObject != this.gameObject && other.isTrigger == false) {
			neighbours.Add (other.gameObject);
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			players.Remove(other.gameObject);
		}
		else if (other.tag == "FlockAgent" && other.gameObject != this.gameObject && other.isTrigger == false) {
			neighbours.Remove (other.gameObject);
		}
	}

	// Calculates the direction of this agent based on flocking rules (only)
	private Vector3 calculateFlockDirectionComponent(){
		Vector3 alignment = Vector3.zero;
		Vector3 cohesion = Vector3.zero;
		Vector3 seperation = Vector3.zero;

		if (this.neighbours.Count > 0) {
			foreach (GameObject obj in neighbours) {
				// alignment
				alignment += obj.GetComponent<AgentState> ().getVelocity ();
			
				//cohesion
				cohesion += obj.transform.position;
			
				//seperation
				if(Vector3.Distance(this.transform.position, obj.transform.position) <= seperationThreshold){
					Vector3 temp = obj.transform.position - this.transform.position;
					temp.Normalize();
					seperation += temp;
				}
			}


			alignment /= this.neighbours.Count;
			alignment.Normalize ();

			cohesion /= this.neighbours.Count;
			cohesion = cohesion - this.transform.position;
			cohesion.Normalize ();

			seperation *= -1;
			seperation.Normalize ();

			Vector3 direction = (alignmentWeight * alignment + cohesionWeight * cohesion + seperationWeight * seperation);

			return direction;
		}

		//if this flock is on its own chose a random direction
		Vector2 rand = Random.insideUnitCircle; // <-- this is a bit shite

		return new Vector3 (rand.x, 0.0f, rand.y);
	}

	private Vector3 calculatePlayerDirectionComponent(){
		Vector3 direction = Vector3.zero;
		float distanceToNearestPlayer = -1.0f;
		if (this.players.Count > 0) {

			foreach(GameObject player in players){
				// if any player in range has flashed run away from them (TODO: should possible change this to run from the nearest one that has flashed)
				if(player.GetComponent<CharacterState>().getLightRadius() >= this.playerLightRunThreshold)
					return this.transform.position - player.transform.position;
				else if(Vector3.Distance(this.transform.position, player.transform.position) < distanceToNearestPlayer || distanceToNearestPlayer == -1){
					if(neighbours.Count >= this.playerChaseThreshold){
						direction = player.transform.position - this.transform.position;
					} else {
						direction = this.transform.position - player.transform.position;
					}
					distanceToNearestPlayer = Vector3.Distance(this.transform.position, player.transform.position);
				}
			}
		}
		return direction;
	}

	private void tryAttackPlayer(){
		foreach (GameObject player in players) {
			if(Vector3.Distance(this.transform.position, player.transform.position) <= attackRange){
				print ("smack dat bitch");
			}
		}
	}

	void Update(){
		Vector3 direction = this.calculateFlockDirectionComponent () + this.calculatePlayerDirectionComponent ();
		direction.Normalize ();
		this.agentMovement.moveInDirection(direction);
		this.agentMovement.move ();
		this.tryAttackPlayer ();
	}
}
