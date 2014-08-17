using UnityEngine;
using System.Collections;

public class ThirdPersonCameraController : MonoBehaviour {
	private CharacterState characterState;

	private float theta = 0.0f;
	private float phi = 0.0f;
	private float radius = 5.0f;
	public float controllerSensitivity = 0.05f;

	public float phiLowerBound = 10.0f;
	public float phiUpperBound = 80.0f;

	public Transform target;

	void Start() {
		characterState = target.gameObject.GetComponent<CharacterState>();
	}

	void Update() {
		if(!characterState.isCameraDirectionLocked()){
			phi += Input.GetAxis ("CameraVertical") * controllerSensitivity;
			theta += Input.GetAxis ("CameraHorizontal") * controllerSensitivity;

			phi = Mathf.Clamp (phi, Mathf.Deg2Rad * phiLowerBound, Mathf.Deg2Rad * phiUpperBound);
			theta %= Mathf.Deg2Rad * 360;

			// Calculate offset of camera from player.
			/* The camera is on the surface of a sphere of radius r with 
			 * inclination theta and azimuth phi.
			 * x = r * cos(theta) * sin(phi)
			 * y = r * cos(phi)
			 * z = r * sin(theta) * sin(phi)
			*/
			Vector3 offset = new Vector3 (
				radius * Mathf.Cos (theta) * Mathf.Sin (phi),
				radius * Mathf.Cos (phi),
				radius * Mathf.Sin (theta) * Mathf.Sin (phi)
			);

			this.transform.position = target.position + offset;

			this.transform.LookAt (target.position);
		}
	}
}
