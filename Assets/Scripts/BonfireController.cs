using UnityEngine;
using System.Collections;

public class BonfireController : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			other.gameObject.GetComponent<CharacterState>().setRespawnPosition(this.transform.position);
		}
	}
}
