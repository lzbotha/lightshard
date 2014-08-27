﻿using UnityEngine;
using System.Collections;

public class LightShardMovement : BasicMovement {

	public float arcHeight = 2.0f;
	public float throwDistance = 3.0f;
	public float throwTime = 1.5f;
	private Vector3 throwVelocity = Vector3.zero;

	public void throwLightShard(Vector3 position, Vector3 direction){
		this.transform.position = position;

		// Calculate vertical velocity
		throwVelocity.y = arcHeight/(throwTime * 0.5f) - 0.5f * gravity * (throwTime * 0.5f);

		// calculate the horizontal components
		float speed = throwDistance/throwTime;
		throwVelocity.x = speed * direction.x;
		throwVelocity.z = speed * direction.z;
	}
	

	void Update () {
		CharacterController controller = GetComponent<CharacterController> ();
		throwVelocity.y += this.gravity * Time.deltaTime;

		if (controller.isGrounded) {
				throwVelocity = Vector3.zero;
		}

		controller.Move (Time.deltaTime * (
			// Stop from bouncing off floor constantly.
			new Vector3 (0.0f, -0.01f, 0.0f) +
			throwVelocity
		));
	}
}