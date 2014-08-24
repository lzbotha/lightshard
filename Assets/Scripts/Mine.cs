using UnityEngine;
using System.Collections;

public abstract class Mine : MonoBehaviour {
	public float detonateDelay = 0.5f;
	public float detectionRadius = 3.0f;
	public float armingRadius = 3.0f;

	private GameObject obj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public abstract void onArm();

	public abstract void onDetection();

	void detonate(){
		Vector3 direction = obj.transform.position - this.transform.position;
		direction.Normalize();
		obj.GetComponent<BasicMovement>().applyForce(direction * 30);
	}

	void OnTriggerEnter(Collider other){
		// TODO: this can now be changed to work with layers, where everything 
		// that is in this layer has a BasicMovement script
		if(other.tag == "Player"){
			obj = other.gameObject;
			Invoke("detonate", detonateDelay);
		}
	}
}
