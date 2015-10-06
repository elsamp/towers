using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	public Tower tower;

	public Enemy currentTarget;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(currentTarget != null){

			this.transform.LookAt(currentTarget.transform);
		}
	
	}

	public void AssignTarget(Enemy enemy){

		currentTarget = enemy;
	}

	public void RemoveTarget(){
		currentTarget = null;
	}
}
