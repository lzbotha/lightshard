using UnityEngine;
using System.Collections;

public class ChaseAgentBehaviour : MonoBehaviour {

	public ChaseAgentState chaseAgentState;
	public AgentMovement agentMovement;


	void Update () {

		// if this agent is currently chasing something
		if (this.chaseAgentState.isChasing ()) {
			this.agentMovement.chaseTarget(this.chaseAgentState.getTargetPosition());
			this.agentMovement.move();
		}
	}
}
