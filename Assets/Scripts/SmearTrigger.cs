using UnityEngine;
using System.Collections;

public class SmearTrigger : MonoBehaviour {
	public TutorialHelper tutorialHelper;
	
	void OnTriggerEnter(Collider c) {
		if (tutorialHelper.smearState == TutorialHelper.BEFORE) {
			tutorialHelper.smearState = TutorialHelper.DURING;
		}
	}
}
