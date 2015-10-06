using UnityEngine;
using System.Collections;

public class Round : MonoBehaviour {

	public Enemy enemyPrefab;

	public float squadProximity;		// time between enemies within the squad
	public int squadCount;				// number of enemies in a round
	public float recoveryTime;			// time in seconds before next round starts

	public bool isActive = false;
	private float lastSpawnTime;

	// Use this for initialization
	void Start () {
		lastSpawnTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		if(isActive){

			if(Time.time > lastSpawnTime + squadProximity){
				// spawn location should be a global setting
				Vector3 enemySpawnLocation = new Vector3(-8, 0, 0);
				Instantiate(enemyPrefab, enemySpawnLocation, Quaternion.identity);
				lastSpawnTime = Time.time;
				squadCount --;
			}

			if(squadCount <= 0){
				RoundController.Instance.SetNextRoundStartTime(Time.time + recoveryTime);
				isActive = false;
			}
		}
	}
}
