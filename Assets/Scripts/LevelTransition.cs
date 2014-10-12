using UnityEngine;
using System.Collections;

public class LevelTransition : MonoBehaviour {

	public string levelName;
	public float delay = 0.0f;

	void OnTriggerEnter(Collider other){

		if(other.tag == "Player")
			Invoke("transition", delay);
	}

	private void transition(){
		Application.LoadLevel(levelName);
	}
}
