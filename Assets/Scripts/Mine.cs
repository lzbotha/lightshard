using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Mine : MonoBehaviour {
	public float detonateDelay = 0.5f;
	public float detectionRadius = 3.0f;
	public SphereCollider detectionTrigger;
	public float armingRadius = 3.0f;

	private HashSet<GameObject> objectsInDetectionRange = new HashSet<GameObject>();


	// Use this for initialization
	void Start () {
		detectionTrigger.radius = detectionRadius;
	}

	// Use this method to do things when objects come within the mines detection range
	// e.g. change the colour of the mine to inform the player that it is now active
	public abstract void onDetection();

	// Use this method to do things when objects come within the mines arming range
	// e.g. change the colour of the mine to inform the player that it is going to
	// blow up shortly
	public abstract void onArm();

	// use this method to do things when there are no longer any objects within detection
	// range, e.g. change colour of mine back to normal
	public abstract void onDisarm();

	// Use this method it implement the detonation behaviour, e.g. make things explode
	public abstract void onDetonate(GameObject obj);

	void detonate(){
		// For each object in range apply an explosive force
		foreach(GameObject obj in objectsInDetectionRange){
			onDetonate(obj);
		}
		onDisarm();
	}

	void Update () {
		// If any object comes within arming range trigger the mine
		foreach(GameObject obj in objectsInDetectionRange){
			if(Vector3.Distance(this.transform.position, obj.transform.position) <= armingRadius){
				onArm();
				Invoke("detonate", detonateDelay);
				break;
			}
		}
	}

	void OnTriggerEnter(Collider other){
		// TODO: this can now be changed to work with layers, where everything 
		// that is in this layer has a BasicMovement script
		if(other.tag == "Player"){
			onDetection();
			objectsInDetectionRange.Add(other.gameObject);
		}
	}

	void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			objectsInDetectionRange.Remove(other.gameObject);
			if(objectsInDetectionRange.Count == 0)
				onDisarm();
		}
	}
}
