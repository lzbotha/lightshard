using UnityEngine;
using System.Collections;
using System.Collections.Generic;
	
public class FLockAgentBehaviour : MonoBehaviour {
	public AgentState agentState;
	public AgentMovement agentMovement;

	public float cohesionWeight;
	public float seperationWeight;
	public float alignmentWeight;

	// Agents will attempt to steer away from other agents within this distance
	public float agentSeperationDistance = 2.0f;

	public float basicAttackRange = 0.3f;
	public float basicAttackDamage = 0.3f;
	public float basicAttackCoolDown = 0.2f;
	private float _basicAttackCooldown = 0.0f;

	public int agentsNeededToChasePlayer;
	public float playerLightRunThreshold = 5.5f;

	private HashSet<GameObject> neighbours = new HashSet<GameObject> ();
	private HashSet<GameObject> players = new HashSet<GameObject> ();

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			this.players.Add(other.gameObject);
		}
		else if (other.tag == "FlockAgent" && other.gameObject != this.gameObject && other.isTrigger == false) {
			this.neighbours.Add (other.gameObject);
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			this.players.Remove(other.gameObject);
		}
		else if (other.tag == "FlockAgent" && other.gameObject != this.gameObject && other.isTrigger == false) {
			this.neighbours.Remove (other.gameObject);
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
				if(Vector3.Distance(this.transform.position, obj.transform.position) <= this.agentSeperationDistance){
					Vector3 temp = obj.transform.position - this.transform.position;
					temp.Normalize();
					seperation += temp;
				}
			}

			alignment.Normalize ();

			cohesion /= this.neighbours.Count;
			cohesion = cohesion - this.transform.position;
			cohesion.Normalize ();

			seperation *= -1;
			seperation.Normalize ();

			Vector3 direction = (alignmentWeight * alignment + cohesionWeight * cohesion + seperationWeight * seperation);
			direction.Normalize ();
			return direction;
		}

		//if this flock is on its own head back to its respawn position
		Vector3 dir =  this.agentState.getRespawnPosition () - this.transform.position;
		dir.Normalize();
		return dir;
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
					if(neighbours.Count >= this.agentsNeededToChasePlayer - 1){
						direction = player.transform.position - this.transform.position;
					} else {
						direction = this.transform.position - player.transform.position;
					}
					distanceToNearestPlayer = Vector3.Distance(this.transform.position, player.transform.position);
				}
			}
		}
		direction.Normalize ();
		return direction;
	}

	private void tryAttackPlayer(){
		foreach (GameObject player in players) {
			print (Vector3.Distance(this.transform.position, player.transform.position));
			if(Vector3.Distance(this.transform.position, player.transform.position) <= this.basicAttackRange){
				if(this._basicAttackCooldown <= 0){
					player.GetComponent<CharacterState>().damage(basicAttackDamage);
					this._basicAttackCooldown = this.basicAttackCoolDown;
				} else {
					this._basicAttackCooldown -= Time.deltaTime;
				}
			}
		}
	}

	void Start() {
		// This is required otherwise the agents don't start moving until another moving component comes within trigger range
		// i.e. OnTriggerEnter is disabled for static objects so the flocks don't start moving by themselves.
		this.agentMovement.moveInDirection(Vector3.right);
		this.agentMovement.move ();
	}

	void Update() {
		Vector3 direction = this.calculateFlockDirectionComponent () + this.calculatePlayerDirectionComponent ();
		direction.Normalize ();

		this.agentMovement.moveInDirection(direction);
		this.agentMovement.move ();
		this.tryAttackPlayer ();
	}
}
