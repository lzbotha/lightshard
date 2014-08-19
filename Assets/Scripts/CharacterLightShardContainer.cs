using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class CharacterLightShardContainer{
	private Dictionary<int, GameObject> lightShards = new Dictionary<int, GameObject>();

	private int maxNumberOfLightShards;
	private int largestKey = -1;
	private int numberOfLightShards = 0;

	public CharacterLightShardContainer(int capacity){
		maxNumberOfLightShards = capacity;
	}

	public int getNumberOfLightShards(){ return numberOfLightShards; }

	public List<Vector3> getAllPositions() {
		List<Vector3> result = new List<Vector3>();
		foreach(KeyValuePair<int, GameObject> entry in lightShards)
		{
		    result.Add(entry.Value.transform.position);
		}
		return result;
	}

	// TODO: rename this method to what it actually does
	public List<KeyValuePair<int, Vector3>> getDirectionsToLightShardsFromPosition(Vector3 position){
		List<KeyValuePair<int, Vector3>> result = new List<KeyValuePair<int, Vector3>>();
		foreach(KeyValuePair<int, GameObject> shard in lightShards) {
			Vector3 directionToLightShard = shard.Value.transform.position - position;
			directionToLightShard.y = 0;
			directionToLightShard.Normalize();

			result.Add(new KeyValuePair<int, Vector3>(shard.Key, directionToLightShard));
		}
		return result;
	}

	public bool removeLightShard(int key) {
		bool removed = lightShards.Remove(key);
		if (removed)
			numberOfLightShards--;
		return removed;
	}

	// Insert a light shard and return its key
	public int addLightShard(GameObject lightShard) {
		// If the container is full make space
		if(numberOfLightShards >= maxNumberOfLightShards){
			// remove lightshard with lowest key value
			int minKey = lightShards.Keys.Min();
			GameObject ls;
			lightShards.TryGetValue(minKey, out ls);
			ls.GetComponent<LightShardController>().cleanUp();
		}

		// Add this lightshard to the container
		lightShards.Add(++largestKey, lightShard);
		numberOfLightShards++;
		
		return largestKey;
	}
}
