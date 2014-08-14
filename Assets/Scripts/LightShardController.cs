using UnityEngine;
using System.Collections;

public class LightShardController : MonoBehaviour {

	public float lifeTime = 15.0f;

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
