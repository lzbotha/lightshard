using UnityEngine;
using System.Collections;

public class Trampoline : MonoBehaviour {

	// all tags that can bounce on this trampoline
	public string[] bouncableTags;

	void OnTriggerEnter(Collider other){
		BasicState state = other.gameObject.GetComponent<BasicState>();
		Vector3 velocity = state.getContinuousVelocity()
		+ new Vector3(0, state.getVerticalSpeed(), 0);

		Vector3 up = this.transform.up;

		Vector3 vdash = Vector3.Dot(velocity, up)/Vector3.Dot(up, up) * up;

		state.setVerticalSpeed(state.getVerticalSpeed() - 2 * vdash.y);
		vdash.y = 0;
		state.setContinuousVelocity(state.getContinuousVelocity() - 2 * vdash);
	}

}
