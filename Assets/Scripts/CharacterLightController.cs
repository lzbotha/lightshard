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
		if(characterState.lightRegenRate > 0 && characterState.lightRadius < normalLightRadius){
			characterState.lightRadius += characterState.lightRegenRate * Time.deltaTime;
		} 
		else if (characterState.lightRegenRate < 0){
			if(characterState.lightRadius + characterState.lightRegenRate <= minLightRadius){
				characterState.lightRadius = minLightRadius;
				characterState.lightRegenRate = normalLightRegenRate;
			}
			characterState.lightRadius += characterState.lightRegenRate * Time.deltaTime;
		}
		gameObject.light.range = characterState.lightRadius;
	}
}
