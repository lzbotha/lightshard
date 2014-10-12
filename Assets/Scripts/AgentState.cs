using UnityEngine;
using System.Collections;

public class AgentState : BasicState {

	void Start(){
		this.setRespawnPosition (this.transform.position);
	}
}
