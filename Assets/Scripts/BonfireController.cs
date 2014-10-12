using UnityEngine;
using System.Collections;

public class BonfireController : MonoBehaviour {

	public ParticleSystem characterActivationEffect;
	public ParticleSystem flame;

	public Color notActivated;
	public Color notCurrentlyActive;
	public Color actives;

	public AudioSource audio;

	private int playersAtThisBonfire = 0;

	void Start() {
		this.characterActivationEffect.Stop();
		this.flame.Stop();
		this.flame.startColor = notActivated;
		this.flame.Play();
	}

	void characterActivationStart(GameObject player) {
		// Play bonfire activation effect and set the players respawn point to this bonfire
		this.characterActivationEffect.Play();
		Invoke("characterActivationStop", 3);
		CharacterState cs = player.GetComponent<CharacterState>();
		cs.setRespawnPosition(this.transform.position);
		if (cs.getLastTouchedBonfire () != null)
			cs.getLastTouchedBonfire ().GetComponent<BonfireController> ().noLongerActive ();

		cs.setLastTouchedBonfire(this.gameObject);

		this.flame.Stop();
		this.flame.startColor = actives;
		this.flame.Play();
		this.playersAtThisBonfire++;
	}

	void characterActivationStop() {
		this.characterActivationEffect.Stop();
	}

	void noLongerActive() {
		this.playersAtThisBonfire--;
		if (this.playersAtThisBonfire == 0) {
			this.flame.Stop ();
			this.flame.startColor = notCurrentlyActive;
			this.flame.Play ();
		}
	}

	void OnTriggerStay(Collider other) {
		if(other.tag == "Player"){
			other.gameObject.GetComponentInChildren<CharacterLightController>().disableFlash();
			other.gameObject.GetComponentInChildren<CharacterLightController>().restoreLightRadius();
		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player"){
			this.audio.Play();
			// if the charater has yet to activate a bonfire or activates a new bonfire
			if(other.gameObject.GetComponent<CharacterState>().getLastTouchedBonfire() == null || other.gameObject.GetComponent<CharacterState>().getLastTouchedBonfire() != this.gameObject){
				this.characterActivationStart(other.gameObject);
			}
		}
	}
}
