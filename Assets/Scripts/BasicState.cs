using UnityEngine;
using System.Collections;

public class BasicState : MonoBehaviour {

	private Vector3 currentForwardDirection;
	private float verticalSpeed;
	// continuous velocity object is experiencing in the XZ plane
	private Vector3 continuousVelocity = Vector3.zero;

	public void setCurrentForwardDirection(Vector3 direction) { currentForwardDirection = direction; }
	public Vector3 getCurrentForwardDirection() { return currentForwardDirection; }

	public float getVerticalSpeed() {
		return this.verticalSpeed;
	}

	public void setVerticalSpeed(float verticalSpeed) {
		this.verticalSpeed = verticalSpeed;
	}

	public Vector3 getContinuousVelocity(){
		return continuousVelocity;
	}

	public void setContinuousVelocity(Vector3 velocity){
		continuousVelocity = velocity;
	}
}
