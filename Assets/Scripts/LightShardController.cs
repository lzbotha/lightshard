using UnityEngine;
using System.Collections;

public class LightShardController : MonoBehaviour {

	public float lifeTime = 15.0f;
	// The radius in which the fear/attract effect takes place
	public float effectRadius = 5.0f;

	// TODO: abstract this away to a GlobalState script
	public float gravity = -20.0f;
	public float arcHeight = 2.0f;
	public float throwDistance = 3.0f;
	public float throwTime = 1.5f;
	public Vector3 velocity = Vector3.zero;

	// The character that cast this LightShard
	private GameObject character;
	// The key in the character owning this lightshards lightshard container
	private int key;

	public void getThrown(Vector3 position, Vector3 direction){
		this.transform.position = position;
		if(direction == Vector3.zero){
			direction = character.transform.forward;
		}
		// Calculate vertical velocity
		velocity.y = arcHeight/(throwTime * 0.5f) - 0.5f * gravity * (throwTime * 0.5f);

		// calculate the horizontal components
		float speed = throwDistance/throwTime;
		velocity.x = speed * direction.x;
		velocity.z = speed * direction.z;
	}

	// Mutator method for character
	public void setCharacter(GameObject character) {
		this.character = character;
	}

	// Mutator method for key
	public void setKey(int key) {
		this.key = key;
	}

	public void setLifeTime(float time){ 
		CancelInvoke();
		lifeTime = time;
		Invoke("cleanUp", lifeTime);
	}

	// Method to remove/destroy LightShard
	public void cleanUp() {
		// If this method is invoked in a way other than a lifetime time out cancel the interval
		CancelInvoke();
		// Remove LightShard from the light shard container of the appropriate character
		character.GetComponent<CharacterState>().lightShards.removeLightShard(key);
		Destroy(gameObject);
	}

	// Use this for initialization
	void Start () {
		// Destroy this LightShard in lifeTime seconds
		if(lifeTime > 0)
			Invoke("cleanUp", lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		CharacterController controller = GetComponent<CharacterController> ();
		velocity.y += gravity * Time.deltaTime;

		if (controller.isGrounded) {
				velocity = Vector3.zero;
		}

		controller.Move (Time.deltaTime * (
			// Stop from bouncing off floor constantly.
			new Vector3 (0.0f, -0.01f, 0.0f) +
			velocity
		));
	}
}
