using UnityEngine;
using System.Collections;

public class ChaseAgentBehaviour : MonoBehaviour {

	public ChaseAgentState chaseAgentState;
	public AgentMovement agentMovement;

	protected void tryChase(){
		// if this agent is currently chasing something
		if (this.chaseAgentState.isChasing ()) {
			this.agentMovement.chaseTarget(this.chaseAgentState.getTargetPosition());
			this.agentMovement.move();
		}
	}


	void Update () {
		this.tryChase ();
	}
}
