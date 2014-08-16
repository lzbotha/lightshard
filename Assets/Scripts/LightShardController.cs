using UnityEngine;
using System.Collections;

public class LightShardController : MonoBehaviour {

	public float lifeTime = 15.0f;
	// The character that cast this LightShard
	// TODO: make this a friend variable since it should not be visible in the inspector
	public GameObject character;

	// Method to remove/destroy LightShard
	void cleanUp() {
		print("Destroying LightShard");
	}

	// Use this for initialization
	void Start () {
		// Destroy this LightShard in lifeTime seconds
		Invoke("cleanUp", lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
