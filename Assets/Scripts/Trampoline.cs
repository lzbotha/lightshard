using UnityEngine;
using System.Collections;

public class Trampoline : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		BasicState state = other.gameObject.GetComponent<BasicState>();
		
		Vector3 velocity = state.getVelocity();
		Vector3 up = this.transform.up;

		Vector3 deltaVelocity = Vector3.Dot(velocity, up)/Vector3.Dot(up, up) * up;

		state.setVelocity(state.getVelocity() - 2 * deltaVelocity);
	}

}
