using UnityEngine;
using System.Collections;

public class BasicState : MonoBehaviour {

	private Vector3 currentForwardDirection;
	private Vector3 velocity = Vector3.zero;
	private Vector3 respawnPosition = Vector3.zero;

	public Vector3 getRespawnPosition(){ return respawnPosition; }
	public void setRespawnPosition(Vector3 pos){ this.respawnPosition = pos; }

	// Respawn this component at the respawn position + some small delta
	public void respawn(Vector3 delta = default(Vector3)) {
		this.setVelocity(Vector3.zero);
		this.transform.position = respawnPosition + delta;
	}

	public void setCurrentForwardDirection(Vector3 direction) { currentForwardDirection = direction; }
	public Vector3 getCurrentForwardDirection() { return currentForwardDirection; }

	public Vector3 getVelocity(){ return this.velocity;	}
	public void setVelocity(Vector3 velocity){ this.velocity = velocity; }

	public void setVelocityX(float x){ this.velocity.x = x; }
	public void setVelocityY(float y){ this.velocity.y = y;	}
	public void setVelocityZ(float z){ this.velocity.z = z; }

	public float getVelocityX(){ return this.velocity.x; }
	public float getVelocityY(){ return this.velocity.y; }
	public float getVelocityZ(){ return this.velocity.z; }
}
