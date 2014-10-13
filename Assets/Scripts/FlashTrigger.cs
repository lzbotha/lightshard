using UnityEngine;
using System.Collections;

public class FlashTrigger : MonoBehaviour {
	public TutorialHelper tutorialHelper;
	
	void OnTriggerEnter(Collider c) {
		if (tutorialHelper.flashState == TutorialHelper.BEFORE) {
			tutorialHelper.flashState = TutorialHelper.DURING;
		}
	}
}
