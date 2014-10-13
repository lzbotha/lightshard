using UnityEngine;
using System.Collections;

public class Level1End : EffectTrigger {
	[SerializeField]
	Camera mainCam;
	[SerializeField]
	float timeToEndLevel = 3.0f;
	float timer = 0.0f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter (Collider other){
		if(timer < timeToEndLevel){
			timer += Time.deltaTime;
		}else{
			Application.LoadLevel("2-level");
		}
	}
}
