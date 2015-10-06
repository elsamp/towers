﻿using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	public Town town;

	private static LevelController instance;
	
	public static LevelController Instance
	{
		get {return instance;}
	}
	
	// Use this for initialization
	void Start () {
		if(instance == null){
			instance = this;
		}
		
		FindObjectOfType<EnemyPath>().Init();
		FindObjectOfType<RoundController>().Init();
		FindObjectOfType<GemSelector>().Init();
	}

	public void SpendReputation(float amount){
		town.TakeHit(amount);
	}

	public void EnemyReachedTown(Enemy enemy){
		town.TakeHit(enemy.townDamageAmount);
	}

	public void EnemyKilled(Enemy enemy){
		town.Heal(enemy.killRewardAmount);
	}

	public void EndLevel(bool isSuccessful){

		if(isSuccessful){
			// go on with game mechanics
		} else {
			RoundController.Instance.StopCurrentRound();
		}
	}
}
