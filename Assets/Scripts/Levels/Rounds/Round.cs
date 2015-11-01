using UnityEngine;
using System.Collections;

public class Round : MonoBehaviour {

	public Enemy enemyPrefab;

	public float squadProximity;		    // time between enemies within the squad
    public int favourReward;
	public int squadCount;				    // number of enemies in a round
    private int activeEnimies;                  // number of enemies in a round

    public float startDelayTime;			// start time in seconds after previous round starts
    public EnemyPath roundPath;

	public bool isActive = false;
	private float lastSpawnTime;

	// Use this for initialization
	void Start () {
		lastSpawnTime = Time.time;
        activeEnimies = squadCount;
    }
	
	// Update is called once per frame
	void Update () {

		if(isActive){

			if(Time.time > lastSpawnTime + squadProximity){
                // spawn location should be a global setting
                Vector3 enemySpawnLocation = roundPath.getNextNode(-1).transform.position;
				Enemy enemy = Instantiate(enemyPrefab, enemySpawnLocation, Quaternion.identity) as Enemy;
                enemy.Path = roundPath;
                enemy.Round = this;
				lastSpawnTime = Time.time;
				squadCount --;
			}

			if(squadCount <= 0){
				//RoundController.Instance.SetNextRoundStartTime(Time.time + recoveryTime);
				isActive = false;
			}
		}
	}

    public void decreaseActiveEnemies()
    {
        activeEnimies--;

        if (activeEnimies <= 0)
        {
            LevelController.Instance.AwardRoundFavour(favourReward);
        }
    }
}
