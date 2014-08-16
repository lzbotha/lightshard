using UnityEngine;
using System.Collections;

public class LightShardController : MonoBehaviour {

	public float lifeTime = 15.0f;
	// The character that cast this LightShard
	// TODO: make this a friend variable since it should not be visible in the inspector
	public GameObject character;

	// Method to remove/destroy LightShard
	void cleanUp() {
		// Remove LightShard from the light shard container of the appropriate character
		character.GetComponent<CharacterState>().lightShards.removeLightShard("test");
		Destroy(GameObject);
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
