using UnityEngine;
using System.Collections;

public class RoundController : MonoBehaviour {

	public Round[] rounds;

	private int nextRoundIndex = 0;
	private float roundStartTime;

	private static RoundController instance;
	
	public static RoundController Instance
	{
		get{return instance;}
	}
	
	public void Init () {
		
		if(instance == null){
			instance = this;
			roundStartTime = Time.time;
		}
	}

	// Update is called once per frame
	void LateUpdate () {

		if((nextRoundIndex < rounds.Length) && (nextRoundIndex <=0 || !rounds[nextRoundIndex -1].isActive)){
			if(Time.time > roundStartTime){
				StartNextRound();
			}
		}
	}

	public void SetNextRoundStartTime(float startTime){
		roundStartTime = startTime;
	}

	public void StartNextRound(){
		rounds[nextRoundIndex].isActive = true;
		nextRoundIndex++;
	}

	public void StopCurrentRound(){
		rounds[nextRoundIndex -1].isActive = false;

		Enemy[] enemies = FindObjectsOfType<Enemy>();

		foreach(Enemy enemy in enemies){
			Destroy(enemy);
		}
	}
}
