using UnityEngine;
using System.Collections;

public class SmearTrail : MonoBehaviour {
	[SerializeField]
	private CharacterMovement moveScript;
	[SerializeField]
	private TrailRenderer trail1;
	[SerializeField]
	private TrailRenderer trail2;
	// Use this for initialization
	void Start () {
		trail1.enabled = false;
		trail2.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(moveScript.isSmearing() && !trail1.enabled){
			trail1.enabled = true;
			trail2.enabled = true;
		}else if(!moveScript.isSmearing() && trail1.enabled){
			trail1.enabled = false;
			trail2.enabled = false;
		}
	}
}
