using UnityEngine;
using System.Collections;

public class MushroomMine : ExplosiveMine {
	public GameObject shroom;

	public override void onDisarm(){
		shroom.renderer.material.color = Color.grey;
	}

	public override void onDetection(){
		shroom.renderer.material.color = Color.yellow;
	}

	public override void onArm(){
		shroom.renderer.material.color = Color.red;
	}

	public override void onDetonate(GameObject obj){
		base.onDetonate (obj);

	}
}
