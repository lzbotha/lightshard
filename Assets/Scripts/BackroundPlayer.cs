using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class BackroundPlayer : MonoBehaviour {
	public AudioClip[] clips;

	IEnumerator loopAllClips() {
		while (true) {
			if (!(this.clips.Length > 0)) {
				throw new System.Exception("Need audio clips for backround player.");
			}

			foreach (AudioClip clip in this.clips) {
				this.audio.clip = clip;
				this.audio.Play();
				yield return new WaitForSeconds(clip.length);
			}
		}
	}

	void Start() {
		StartCoroutine(loopAllClips());
	}

	void Update () {
	}
}
