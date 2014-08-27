using UnityEngine;
using System.Collections;

public class BasicMovement : MonoBehaviour {
	
	public float gravity;
	public float jumpSpeed;
	public float speed;
	public float mass = 1.0f;

	public float dampening = 50.0f;

	private BasicState associatedState;

	void Start(){
		associatedState = this.GetComponent<BasicState>();
	}

	public Vector3 getVelocityComponent(){
		Vector3 velocity = associatedState.getContinuousVelocity();
		if(velocity.x > 0)
			velocity.x = Mathf.Clamp(velocity.x - this.dampening * Time.deltaTime, 0, Mathf.Infinity);
		else if (velocity.x < 0)
			velocity.x = Mathf.Clamp(velocity.x + this.dampening * Time.deltaTime, Mathf.NegativeInfinity, 0);
		if(velocity.z > 0)	
			velocity.z = Mathf.Clamp(velocity.z - this.dampening * Time.deltaTime, 0, Mathf.Infinity);
		else if (velocity.z < 0)
			velocity.z = Mathf.Clamp(velocity.z + this.dampening * Time.deltaTime, Mathf.NegativeInfinity, 0);
		return Time.deltaTime * velocity;
	}

	public void applyForce(Vector3 force){
		associatedState.setVerticalSpeed(associatedState.getVerticalSpeed() + (force.y / this.mass));
		force.y = 0;
		associatedState.setContinuousVelocity((1/this.mass) * force);
	}

}
