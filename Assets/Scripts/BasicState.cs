using UnityEngine;
using System.Collections;

public class BasicState : MonoBehaviour {

	private Vector3 currentForwardDirection;
	private float verticalSpeed;

	public void setCurrentForwardDirection(Vector3 direction) { currentForwardDirection = direction; }
	public Vector3 getCurrentForwardDirection() { return currentForwardDirection; }

	public float getVerticalSpeed() {
		return this.verticalSpeed;
	}

	public void setVerticalSpeed(float verticalSpeed) {
		this.verticalSpeed = verticalSpeed;
	}
}
