using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			Vector3 direction = other.gameObject.transform.position - this.transform.position;
			direction.Normalize();
			other.gameObject.GetComponent<CharacterMovement>().applyVelocity(direction * 30);
		}
	}
}
