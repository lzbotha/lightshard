using UnityEngine;
using System.Collections;

public class BonfireTrigger : MonoBehaviour {
	public TutorialHelper tutorialHelper;
	
	void OnTriggerEnter(Collider c) {
		if (tutorialHelper.bonfireState == TutorialHelper.BEFORE) {
			tutorialHelper.bonfireState = TutorialHelper.DURING;
		}
	}
}