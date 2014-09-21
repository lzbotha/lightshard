using UnityEngine;
using System.Collections;

public class ChaseAgentState : MonoBehaviour {

	public float maxChaseSeperation;

	private GameObject targetPlayer;
	private bool chasing = false;

	void OnTriggerEnter(Collider other){
		// if this agent is not chasing a player and a player enters its observation range
		if (!this.chasing && other.tag == "Player") {
			this.targetPlayer = other.gameObject;
		}
	}

	void Update(){
		if (chasing) {
			if(Vector3.Distance(this.transform.position, targetPlayer.transform.position) > maxChaseSeperation){
				chasing = false;
			}
		}
	}
}
