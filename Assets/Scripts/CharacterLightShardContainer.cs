using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterLightShardContainer{
	private Dictionary<int, GameObject> lightShards = new Dictionary<int, GameObject>();

	int maxLightShards = 8;
	private int largestKey = -1;

	public bool removeLightShard(int key) {
		return lightShards.Remove(key);
	}

	// Insert a light shard and return its key
	public int addLightShard(GameObject lightShard) {
		lightShards.Add(++largestKey, lightShard);
		return largestKey;
	}
}
