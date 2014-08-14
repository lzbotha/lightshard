using UnityEngine;
using System.Collections;

public class CharacterLightController : MonoBehaviour {

	private CharacterState characterState;

	public float minLightRadius = 2.0f;
	public float normalLightRadius = 5.0f;
	public float normalLightRegenRate = 0.5f;

	// Use this for initialization
	void Start () {
		characterState = this.transform.parent.GetComponent<CharacterState>();
	}
	
	// Update is called once per frame
	void Update () {
		
		gameObject.light.range = characterState.lightRadius;
	}
}
