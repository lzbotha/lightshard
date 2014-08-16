using UnityEngine;
using System.Collections;

public class LightShardController : MonoBehaviour {

	public float lifeTime = 15.0f;
	// The character that cast this LightShard
	// TODO: make these friend variable since it should not be visible in the inspector
	// OR stop being lazy and just make accessor and mutator methods like a normal human being
	public GameObject character;
	public int key;

	// Method to remove/destroy LightShard
	void cleanUp() {
		// Remove LightShard from the light shard container of the appropriate character
		character.GetComponent<CharacterState>().lightShards.removeLightShard(key);
		Destroy(gameObject);
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
