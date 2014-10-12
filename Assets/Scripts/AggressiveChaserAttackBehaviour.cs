using UnityEngine;
using System.Collections;

public class AggressiveChaserAttackBehaviour : MonoBehaviour {
	[SerializeField]
	GameObject enemyAttack;
	[SerializeField]
	float attackPeriod;
	[SerializeField]
	float attackSpeed;
	float timer = 0.0f;
	Transform player;
	SphereCollider sphereCollider;
	// Use this for initialization
	void Start () {
		sphereCollider = transform.GetComponent<SphereCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		if(player != null && Vector3.Distance(transform.position, player.position) < sphereCollider.radius && timer <= attackPeriod){
			timer += Time.deltaTime;
		}

		if(player != null && Vector3.Distance(transform.position, player.position) < sphereCollider.radius && timer >= attackPeriod){
			GameObject attack = Instantiate(enemyAttack, this.transform.position, Quaternion.LookRotation(player.position)) as GameObject;
			Vector3 toPlayer = player.position - transform.position;
			toPlayer.Normalize();
			attack.rigidbody.AddForce(toPlayer*attackSpeed);
			timer = 0.0f;
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Player"){
			player = other.gameObject.transform;
		}
	}
}
