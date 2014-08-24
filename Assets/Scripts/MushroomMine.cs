using UnityEngine;
using System.Collections;

public class MushroomMine : Mine {

	public override void disarm(){
		this.renderer.material.color = Color.grey;
	}

	public override void onArm(){
		this.renderer.material.color = Color.red;
	}

	public override void onDetection(){
		this.renderer.material.color = Color.yellow;
	}
}
