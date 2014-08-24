using UnityEngine;
using System.Collections;

public class MushroomMine : Mine {

	public override void disarm(){
		this.renderer.material.color = Color.grey;
	}

	public override void onDetection(){
		print("bitch should be yellow");
		this.renderer.material.color = Color.yellow;
	}

	public override void onArm(){
		this.renderer.material.color = Color.red;
	}
}
