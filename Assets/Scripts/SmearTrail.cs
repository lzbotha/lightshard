using UnityEngine;
using System.Collections;

public class SmearTrail : MonoBehaviour {
	[SerializeField]
	private CharacterMovement moveScript;
	[SerializeField]
	private TrailRenderer trail1;
	[SerializeField]
	private float afterSmearTime;
	private float timer;
//	[SerializeField]
//	private TrailRenderer trail2;
	// Use this for initialization
	void Start () {
		trail1.renderer.enabled = false;
		trail1.enabled = false;
//		trail2.enabled = false;
		//renderer.material.shader = Shader.Find("ItemGlow");
	}
	
	// Update is called once per frame
	void Update () {
		if(moveScript.isSmearing()){
			renderer.material.SetFloat("_Factor", 1.0f);
			if(!trail1.enabled){
				trail1.renderer.enabled = true;
				trail1.enabled = true;
//				trail2.enabled = true;
				timer = 0f;
			}
		}else if(!moveScript.isSmearing()){
			renderer.material.SetFloat("_Factor", 0.0f);

			if(trail1.enabled && timer >= afterSmearTime){
				trail1.renderer.enabled = false;
				trail1.enabled = false;
//				trail2.enabled = false;
			}else if(trail1.enabled){
				timer += Time.deltaTime;
			}
		}
	}
}
