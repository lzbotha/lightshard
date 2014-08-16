using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterLightShardContainer{
	private Dictionary<string, GameObject> lightShards = new Dictionary<string, GameObject>();

	int maxLightShards = 8;

	public bool removeLightShard(string key) {
		return lightShards.Remove(key);
	}

	// Insert a light shard and return its key
	public void addLightShard(string key, GameObject lightShard) {
		lightShards.Add(key, lightShard);
	}
}
