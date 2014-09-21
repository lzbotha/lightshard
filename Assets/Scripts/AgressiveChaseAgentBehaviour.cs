using UnityEngine;
using System.Collections;

public class AgressiveChaseAgentBehaviour : ChaseAgentBehaviour {

	public float basicAttackRange;
	public float basicAttackDamage;
	public float basicAttackCooldown;
	private float _basicAttackCooldown;

	protected void tryAttack(){
		if(this.chaseAgentState.isChasing()){
			if(_basicAttackCooldown <= 0 && Vector3.Distance(this.chaseAgentState.getTargetPosition(), this.transform.position) <= this.basicAttackRange){

				this.chaseAgentState.getTarget().GetComponent<CharacterState>().damage(basicAttackDamage);
				
				this._basicAttackCooldown = this.basicAttackCooldown;
			}
		}
	}

	void Update(){
		this._basicAttackCooldown -= Time.deltaTime;
		this.tryChase ();
		this.tryAttack ();
	}
}
