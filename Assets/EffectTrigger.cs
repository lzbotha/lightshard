using UnityEngine;
using System.Collections;

public class EffectTrigger : MonoBehaviour {

	public ParticleSystem[] effects;

	void Start(){
		foreach(ParticleSystem effect in effects)
			effect.Stop ();
	}

	void OnTriggerEnter(Collider other){

		if(other.tag == "Player"){
			foreach(ParticleSystem effect in effects)
				effect.Play ();
		}
	}
}
