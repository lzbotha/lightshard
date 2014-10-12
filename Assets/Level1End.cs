using UnityEngine;
using System.Collections;

public class Level1End : MonoBehaviour {
	[SerializeField]
	GameObject Ray1;
	[SerializeField]
	GameObject Ray2;
	[SerializeField]
	GameObject Sparkles;
	[SerializeField]
	GameObject Wind;
	[SerializeField]
	float changeSpeed = 3.0f;

	Color rayColorStart = new Color (128, 128, 128, 128);
	Color sparklesColorStart = new Color(255, 255, 255, 150);
	Color windColorStart = new Color(128, 128, 128);
	Color rayColorEnd = new Color(0, 139, 255, 128);
	Color sparklesColorEnd = new Color(0, 255, 192, 128);
	Color windColorEnd = new Color(125, 255, 0, 128);

	bool atGoal = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Color rayColorCurrent = Ray1.GetComponent<ParticleSystem>().renderer.material.GetColor("_TintColor");
		Color sparklesColorCurrent = Sparkles.GetComponent<ParticleSystem>().renderer.material.GetColor("_TintColor");
		Color windColorCurrent = Wind.GetComponent<ParticleSystem>().renderer.material.GetColor("_TintColor");
		if(atGoal){
			Ray1.GetComponent<ParticleSystem>().renderer.material.SetColor("_TintColor", Color.Lerp(rayColorCurrent, rayColorEnd, changeSpeed*Time.deltaTime));
			Ray2.GetComponent<ParticleSystem>().renderer.material.SetColor("_TintColor", Color.Lerp(rayColorCurrent, rayColorEnd, changeSpeed*Time.deltaTime));
			Sparkles.GetComponent<ParticleSystem>().renderer.material.SetColor("_TintColor", Color.Lerp(sparklesColorCurrent, sparklesColorEnd, changeSpeed*Time.deltaTime));
			Wind.GetComponent<ParticleSystem>().renderer.material.SetColor("_TintColor", Color.Lerp(windColorCurrent, windColorEnd, changeSpeed*Time.deltaTime));
		}else{
			Ray1.GetComponent<ParticleSystem>().renderer.material.SetColor("_TintColor", Color.Lerp(rayColorCurrent, rayColorStart, changeSpeed*Time.deltaTime));
			Ray2.GetComponent<ParticleSystem>().renderer.material.SetColor("_TintColor", Color.Lerp(rayColorCurrent, rayColorStart, changeSpeed*Time.deltaTime));
			Sparkles.GetComponent<ParticleSystem>().renderer.material.SetColor("_TintColor", Color.Lerp(sparklesColorCurrent, sparklesColorStart, changeSpeed*Time.deltaTime));
			Wind.GetComponent<ParticleSystem>().renderer.material.SetColor("_TintColor", Color.Lerp(windColorCurrent, windColorStart, changeSpeed*Time.deltaTime));
		}

	}

	void OnTriggerEnter (Collider other){
		if(other.gameObject.tag == "Player"){
			atGoal = true;
		}
	}

	void OnTriggerExit (Collider other){
		if(other.gameObject.tag == "Player"){
			atGoal = false;
		}
	}
}
