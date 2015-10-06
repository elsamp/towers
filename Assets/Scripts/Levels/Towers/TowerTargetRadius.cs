using UnityEngine;
using System.Collections;

public class TowerTargetRadius : MonoBehaviour {

	public Tower tower;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		Enemy enemyTarget = other.GetComponent<Enemy>();
		tower.AddPotentailTarget(enemyTarget);
	}
}
