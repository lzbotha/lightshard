using UnityEngine;
using System.Collections;

public class SmearChecker : MonoBehaviour {
	private Vector3 target;
	private bool collided;
	private float smearSpeed = 8.0f;
	private CharacterState characterState;
	// Use this for initialization
	void Start () {
		characterState = gameObject.GetComponentInParent<CharacterState>();
		collided = false;
		this.GetComponent<MeshRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Smear")){
			Debug.Log("SC smear");
			//			this.rigidbody.AddForce(Quaternion.LookRotation (movementDirection) * input * 300);
			Vector3 p1 = transform.parent.position + transform.forward*characterState.lightRadius;
			transform.position = p1;
			if(!collided){
				target = transform.position;
				Debug.Log(transform.parent.position+" to => "+target);

				//change layer of character and all its children
				Transform [] children = transform.parent.gameObject.GetComponentsInChildren<Transform>();
				foreach (Transform child in children){
					child.gameObject.layer = LayerMask.NameToLayer("PlayerSmear");
				}
				transform.parent.gameObject.layer = LayerMask.NameToLayer("PlayerSmear");
			}
		}

		if(target != Vector3.zero){
			Debug.Log("target not zero = "+Vector3.Distance(transform.parent.position, target));
			if(Vector3.Distance(transform.parent.position, target) > 0.1f){
				transform.parent.position = Vector3.Lerp(transform.parent.position, target, smearSpeed*Time.deltaTime);
			}else{
				Debug.Log("changing target to zero");
				target = Vector3.zero;
				transform.localPosition = Vector3.zero;
				//change layer of character and all its children
				Transform [] children = transform.parent.gameObject.GetComponentsInChildren<Transform>();
				foreach (Transform child in children){
					child.gameObject.layer = LayerMask.NameToLayer("Default");
				}
				transform.parent.gameObject.layer = LayerMask.NameToLayer("Default");
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.tag != "Player" && other.tag != "Floor"){
			collided = true;
		}
	}

	void OnTriggerExit(Collider other){
		if(other.tag != "Player"){
			collided = false;
		}
	}
}
