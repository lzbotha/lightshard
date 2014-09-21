using UnityEngine;
using System.Collections;

public class LightShardState : BasicState {
	private bool landed = false;

	public bool hasLanded() { return this.landed; }
	public void setLanded(bool b) { this.landed = b; }
}
