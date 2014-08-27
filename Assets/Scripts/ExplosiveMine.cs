using UnityEngine;
using System.Collections;

public abstract class ExplosiveMine : Mine {
	public Vector3 explosiveForce = Vector3.zero;

	public override void onDetonate(GameObject obj){
		Vector3 direction = obj.transform.position - this.transform.position;
		direction.Normalize();
		print("applying force");
		obj.GetComponent<BasicMovement>().applyForce(new Vector3(
			direction.x * this.explosiveForce.x,
			direction.y * this.explosiveForce.y,
			direction.z * this.explosiveForce.z
		));
	}
	
}
