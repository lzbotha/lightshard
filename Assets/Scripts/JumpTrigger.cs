using UnityEngine;
using System.Collections;

public class JumpTrigger : MonoBehaviour {
	public TutorialHelper tutorialHelper;

	void OnTriggerEnter(Collider c) {
		if (tutorialHelper.jumpState == TutorialHelper.BEFORE) {
			tutorialHelper.jumpState = TutorialHelper.DURING;
		}
	}
}
