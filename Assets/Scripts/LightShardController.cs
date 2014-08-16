using UnityEngine;
using System.Collections;

public class LightShardController : MonoBehaviour {

	public float lifeTime = 15.0f;
	// The character that cast this LightShard
	private GameObject character;
	// The key in the character owning this lightshards lightshard container
	private int key;

	// Mutator method for character
	public void setCharacter(GameObject character) {
		this.character = character;
	}

	// Mutator method for key
	public void setKey(int key) {
		this.key = key;
	}

	// Method to remove/destroy LightShard
	public void cleanUp() {
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
