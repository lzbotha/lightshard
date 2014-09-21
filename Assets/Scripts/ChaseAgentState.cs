using UnityEngine;
using System.Collections;

public class ChaseAgentState : AgentState {

	public float maxChaseSeperation;

	private GameObject target;
	private bool chasing = false;

	public bool isChasing() { return chasing; }

	public Vector3 getTargetPosition() { return this.target.transform.position; }

	void OnTriggerEnter(Collider other){
		// if this agent is not chasing a player and a player enters its observation range
		if (!this.chasing && other.tag == "Player") {
			print ("target aquired");
			this.target = other.gameObject;
			this.chasing = true;
		}
	}

	void Update () {
		if (chasing) {
			if(Vector3.Distance(this.transform.position, target.transform.position) > maxChaseSeperation){
				print ("target lost");
				chasing = false;
			}
		}
	}
}
