using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterState : BasicState {
	// Class to hold all state of the character that needs to be accessed
	// by multiple scripts or objects
	
	// These values are determined elsewhere and set in the Start() method
	// Changing them here will have no effect

	public bool inAir = false;
	public float lightRadius;
	private float minLightRadius = 2.0f;
	private float lightRegenRate;	
	private float flashDeactivationRadius;

	// Can the character currently change direction in the XZ plane
	public bool movementDirectionLocked;

	private bool cameraDirectionLocked;

	public CharacterLightShardContainer lightShards = new CharacterLightShardContainer(10);
	
	public int latestLightShardID = -1;

	public ParticleSystem respawnEffect;
	private GameObject lastTouchedBonfire;

	public void setLastTouchedBonfire(GameObject bonfire){ lastTouchedBonfire = bonfire; }
	public GameObject getLastTouchedBonfire(){ return lastTouchedBonfire; }

	public override void respawn(Vector3 delta = default(Vector3)) {
		base.respawn(delta);
		this.respawnEffect.Play();
	}

	public float getLightRadius(){ return lightRadius; }
	public void setLightRadius(float radius){ lightRadius = radius; }
	public void changeLightRadiusBy(float amount){ lightRadius += amount;}	

	public float getMinLightRadius(){ return minLightRadius; }
	public void setMinLightRadius(float radius){ minLightRadius = radius; }

	public float getLightRegenRate(){ return lightRegenRate; }
	public bool isLightRegenPositive() { return lightRegenRate > 0; }
	public bool isLightRegenNegative() { return lightRegenRate < 0;	}
	public void setLightRegenRate(float regen){ lightRegenRate = regen;	}

	public void setFlashDeactivationRadius(float radius){ flashDeactivationRadius = radius; }
	public float getFlashDeactivationRadius(){ return flashDeactivationRadius; }

	public bool isMovementDirectionLocked(){ return movementDirectionLocked; }
	public void setMovementDirectionLocked(bool locked){ movementDirectionLocked = locked; }

	public void setCameraDirectionLocked(bool locked) { cameraDirectionLocked = locked; }
	public bool isCameraDirectionLocked() {return cameraDirectionLocked; }

	public bool canUseAbility(float cost) {
		return lightRadius - cost >= minLightRadius;
	}

	void Start() {
		this.respawnEffect.Stop();
		movementDirectionLocked = false;
		flashDeactivationRadius = 0;
		cameraDirectionLocked = false;
	}

}
