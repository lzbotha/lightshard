using UnityEngine;
using System.Collections;

public class AgentState : BasicState {

	public Pathfinding pf;
	public float distToWaypoint = 0.1f;

	public void findPathTo(Vector3 target){
		pf.FindPath(this.transform.position, target);
	}

	public Vector3 getNextPathSegment(){
		// if there are waypoints remaining in the path
		if (pf.Path.Count > 0) {
			Vector3 path = pf.Path [0];

			// if this agent is within distToWaypoint of the current waypoint remove it from the path
			if (Vector3.Distance (this.transform.position, pf.Path [0]) < this.distToWaypoint) {
				pf.Path.RemoveAt (0);
			}
			return path;
		}
		return this.transform.position;
	}
}
