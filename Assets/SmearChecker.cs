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
			Debug.Log("target not zero = "+transform.parent.position+"vs"+target);
			if(characterState.inAir && (Mathf.Abs(transform.parent.position.x - target.x) > 0.5f || Mathf.Abs(transform.parent.position.z - target.z) > 0.5f)){
				target.y = transform.parent.position.y;
				transform.parent.position = Vector3.Lerp(transform.parent.position, target, smearSpeed*Time.deltaTime);
			}else if(characterState.inAir){
				target = Vector3.zero;
				transform.localPosition = Vector3.zero;
				//change layer of character and all its children
				Transform [] children = transform.parent.gameObject.GetComponentsInChildren<Transform>();
				foreach (Transform child in children){
					child.gameObject.layer = LayerMask.NameToLayer("Default");
				}
				transform.parent.gameObject.layer = LayerMask.NameToLayer("Default");
			}else if(Mathf.Abs(transform.parent.position.x - target.x) > 0.1f || Mathf.Abs(transform.parent.position.z - target.z) > 0.1f){
				target.y = transform.parent.position.y;
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
