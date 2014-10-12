using UnityEngine;
using System.Collections;

public class MeanPosition : MonoBehaviour {
	
	public Transform[] transforms;

	// Update is called once per frame
	void Update () {		
		Vector3 average = Vector3.zero;
		foreach (Transform transform in this.transforms) {
			average += transform.position;
		}

		average /= this.transforms.Length;

		this.transform.position = average;
	}
}
