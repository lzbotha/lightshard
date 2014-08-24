using UnityEngine;
using System.Collections;

public class MushroomMine : Mine {
	public GameObject shroom;

	public override void disarm(){
		shroom.renderer.material.color = Color.grey;
	}

	public override void onDetection(){
		shroom.renderer.material.color = Color.yellow;
	}

	public override void onArm(){
		shroom.renderer.material.color = Color.red;
	}
}
