using UnityEngine;
using System.Collections;

public class BonfireController : MonoBehaviour {

	public ParticleSystem characterActivationEffect;

	void Start() {
		this.characterActivationEffect.Stop();
	}

	void characterActivationStart(GameObject player) {
		// Play bonfire activation effect and set the players respawn point to this bonfire
		this.characterActivationEffect.Play();
		Invoke("characterActivationStop", 3);
		player.GetComponent<CharacterState>().setRespawnPosition(this.transform.position);
	}

	void characterActivationStop() {
		this.characterActivationEffect.Stop();
	}

	void OnTriggerStay(Collider other) {
		if(other.tag == "Player"){
			other.gameObject.GetComponentInChildren<CharacterLightController>().disableFlash();
			other.gameObject.GetComponentInChildren<CharacterLightController>().restoreLightRadius();
		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player"){
			// TODO: change this to work with distance
			if(other.transform.position != this.transform.position){
				characterActivationStart(other.gameObject);
			}
		}
	}
}
