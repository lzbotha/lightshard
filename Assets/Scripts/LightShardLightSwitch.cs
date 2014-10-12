using UnityEngine;
using System.Collections;

public class LightShardLightSwitch : MonoBehaviour {

	public Light spotLight;
	private bool isOn = false;

	void OnTriggerEnter(Collider other){
		if (!this.isOn){
			print (other.tag);
			if (other.tag == "LightShard") {
				print("Lights on");
				spotLight.intensity = 8.0f;
				this.isOn = true;
			}
		}
	}
}
