using UnityEngine;
using System.Collections;

public class RoundController : MonoBehaviour {

	public Round[] rounds;

	private int nextRoundIndex = 0;
	private float nextRoundStartTime;

	private static RoundController instance;
	
	public static RoundController Instance
	{
		get{return instance;}
	}
	
	public void Init () {
		
		if(instance == null){
			instance = this;
            nextRoundStartTime = Time.time;
		}
	}

	// Update is called once per frame
	void LateUpdate () {

		if((nextRoundIndex < rounds.Length) && (Time.time > nextRoundStartTime))
        {
            StartNextRound();
		}
	}

	public void SetNextRoundStartTime(){

        if ((nextRoundIndex < rounds.Length)){
            nextRoundStartTime = Time.time + rounds[nextRoundIndex].startDelayTime;
        } 
	}

	public void StartNextRound(){
		rounds[nextRoundIndex].isActive = true;
		nextRoundIndex++;
        SetNextRoundStartTime();
    }

	public void StopCurrentRound(){
		rounds[nextRoundIndex -1].isActive = false;

		Enemy[] enemies = FindObjectsOfType<Enemy>();

		foreach(Enemy enemy in enemies){
			Destroy(enemy);
		}
	}
}
