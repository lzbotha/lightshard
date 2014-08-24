using UnityEngine;
using System.Collections;

public class BasicMovement : MonoBehaviour {
	
	public float gravity;
	public float jumpSpeed;
	public float speed;
	public float mass = 1.0f;

	public float dampening = 50.0f;
	private Vector3 velocity = Vector3.zero;

	public Vector3 getVelocityComponent(){
		if(velocity.x > 0)
			velocity.x = Mathf.Clamp(velocity.x - dampening * Time.deltaTime, 0, Mathf.Infinity);
		else if (velocity.x < 0)
			velocity.x = Mathf.Clamp(velocity.x + dampening * Time.deltaTime, Mathf.NegativeInfinity, 0);
		if(velocity.z > 0)	
			velocity.z = Mathf.Clamp(velocity.z - dampening * Time.deltaTime, 0, Mathf.Infinity);
		else if (velocity.z < 0)
			velocity.z = Mathf.Clamp(velocity.z + dampening * Time.deltaTime, Mathf.NegativeInfinity, 0);
		print(Time.deltaTime * velocity);
		return Time.deltaTime * velocity;
	}

	public void applyForce(Vector3 force){
		BasicState state = this.GetComponent<BasicState>();
		state.setVerticalSpeed(state.getVerticalSpeed() + (force.y / this.mass));
		force.y = 0;
		this.velocity = (1/this.mass) * force;
	}

}
