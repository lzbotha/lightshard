using UnityEngine;
using System.Collections;

public class EffectTrigger : MonoBehaviour {

	public ParticleSystem effect;

	void Start(){
		effect.Stop ();
	}

	void OnTriggerEnter(Collider other){

		if(other.tag == "Player"){
			effect.Play();
		}
	}
}
