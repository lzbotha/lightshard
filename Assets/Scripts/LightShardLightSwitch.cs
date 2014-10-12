using UnityEngine;
using System.Collections;

public class LightShardLightSwitch : MonoBehaviour {

	public Light[] spotLights;
	public GameObject[] hiddenPlatforms;
	private bool isOn = false;

	void Start(){
		foreach(GameObject platform in this.hiddenPlatforms){
			platform.renderer.enabled = false;
			platform.collider.enabled = false;
		}
	}

	void OnTriggerEnter(Collider other){
		if (!this.isOn){
			if (other.tag == "LightShard" || other.tag == "Player") {
				this.isOn = true;

				foreach(Light spot in spotLights){
					spot.intensity = 8.0f;
				}

				foreach(GameObject platform in this.hiddenPlatforms){
					platform.renderer.enabled = true;
					platform.collider.enabled = true;
				}
			}
		}
	}
}
