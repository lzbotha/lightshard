using UnityEngine;
using System.Collections;

public class EnemyAttackController : MonoBehaviour {
	[SerializeField]
	GameObject Explosion;
	CharacterState characterState;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Player"){
			characterState = other.gameObject.GetComponentInParent<CharacterState>();
			characterState.damage(3.0f);
		}
		GameObject att = Instantiate(Explosion, this.transform.position, Quaternion.identity) as GameObject;
		att.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		Destroy(this.gameObject);
	}
}
