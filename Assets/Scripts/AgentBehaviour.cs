using UnityEngine;
using System.Collections;

public class AgentBehaviour : MonoBehaviour {
	public GameObject player;
	public AgentState agentState;
	public AgentMovement agentMovement;


	void Update(){
		this.agentMovement.chaseTarget (player.transform.position);
		this.agentMovement.move ();
	}
}
