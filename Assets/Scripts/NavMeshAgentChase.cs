using UnityEngine;
using System.Collections;

public class NavMeshAgentChase : MonoBehaviour {
	[SerializeField]
	Transform target;
	NavMeshAgent agent;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(target.position, transform.position) < 10){
			Debug.Log("close enough");
			agent.SetDestination(target.position);
		}else{
			Debug.Log("too far - "+Vector3.Distance(target.position, transform.position));
			agent.SetDestination(transform.position);
		}
	}
}
