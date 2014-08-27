using UnityEngine;
using System.Collections;

public class Trampoline : MonoBehaviour {

	// all tags that can bounce on this trampoline
	public string[] bouncableTags;

	void OnTriggerEnter(Collision other){
		print("bounce");
		BasicState state = other.gameObject.GetComponent<BasicState>();
		Vector3 velocity = state.getVelocity();

		Vector3 up = this.transform.up;

		Vector3 vdash = Vector3.Dot(velocity, up)/Vector3.Dot(up, up) * up;

		state.setVelocity(state.getVelocity() - 2 * vdash);
	}

}
