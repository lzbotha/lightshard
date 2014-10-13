using UnityEngine;
using System.Collections;

public class SpawnerController : MonoBehaviour {
	public PlayerState playerPrefab;
	public MeanPosition meanPosition;
	public Vector3 playerOnePosition;
	public Vector3 playerTwoPosition;

	// Use this for initialization
	void Start () {

		if (PlayerPrefs.GetInt("isCoop") == 0) {
			// Single Player.
			Debug.Log ("Single Player");
			PlayerState player = (PlayerState) Instantiate (playerPrefab, this.transform.position + this.playerOnePosition, Quaternion.identity);
			player.player = 1;
			player.playersInLevel =  1;

			meanPosition.transforms[0] = player.transform;			
			meanPosition.transforms[1] = player.transform;

		} else if (PlayerPrefs.GetInt("isCoop") == 1){
			// Coop.
			Debug.Log ("Coop");
			PlayerState player1 = (PlayerState) Instantiate (playerPrefab, this.transform.position + this.playerOnePosition, Quaternion.identity);
			player1.player = 1;
			player1.playersInLevel =  2;

			PlayerState player2 = (PlayerState) Instantiate (playerPrefab, this.transform.position + this.playerTwoPosition, Quaternion.identity);
			player2.player = 2;
			player2.playersInLevel =  2;

			meanPosition.transforms[0] = player1.transform;			
			meanPosition.transforms[1] = player2.transform;

		} else {
			throw new System.Exception("Unknown isCoop value.");
		}
	}
}
