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

	public AudioSource respawnAudioSource;

	// Can the character currently change direction in the XZ plane
	public bool movementDirectionLocked;

	private bool cameraDirectionLocked;

	public int numLightShards = 3;
	public CharacterLightShardContainer lightShards;
	
	public int latestLightShardID = -1;

	public ParticleSystem respawnEffect;
	private GameObject lastTouchedBonfire;

	private string playerTag = "Player 1 - ";

	public float bonfireRespawnOffset = 2.0f;

	public string getPlayerTag(){ return playerTag; }
	public void setPlayerTag(string tag){ this.playerTag = tag; }

	public void setLastTouchedBonfire(GameObject bonfire){ lastTouchedBonfire = bonfire; }
	public GameObject getLastTouchedBonfire(){ return lastTouchedBonfire; }

	public override void respawn(Vector3 delta = default(Vector3)) {
		this.GetComponent<CharacterMovement> ().cancelSmear ();

		this.respawnAudioSource.Play ();

		// Respawn the player somewhere on the circle with radius bonfireRespawnOffset
		Vector2 delta2 = Random.insideUnitCircle;
		delta2.Normalize();
		delta2 *= this.bonfireRespawnOffset;

		respawnEffect.Play ();

		base.respawn(new Vector3(delta2.x, 1.0f, delta2.y));
	}

	public float getLightRadius(){ return lightRadius; }
	public void setLightRadius(float radius){ lightRadius = radius; }
	public void changeLightRadiusBy(float amount){ lightRadius += amount;}
	public void damage(float damage){
		// damage the player
		this.changeLightRadiusBy (-damage);

		// if the player dies respawn them
		if (this.lightRadius <= 0)
			this.respawn ();
	}

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
		this.lightShards = new CharacterLightShardContainer(this.numLightShards);
		this.respawnEffect.Stop();
		movementDirectionLocked = false;
		flashDeactivationRadius = 0;
		cameraDirectionLocked = false;
	}

}
