using UnityEngine;
using System.Collections;

public class LightShardController : MonoBehaviour {

	public float lifeTime = 15.0f;
	// The character that cast this LightShard
	// TODO: make this a friend variable since it should not be visible in the inspector
	public GameObject character;

	// Method to remove/destroy LightShard
	void cleanUp() {

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		lifeTime -= Time.deltaTime;
		if(lifeTime <= 0){
			// TODO: change this to not suck
			Destroy(gameObject);
		}
	}
}
