using UnityEngine;
using System.Collections;

public class ProjectileSplashArea : MonoBehaviour {

	public Projectile projectile;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		Enemy enemyTarget = other.GetComponent<Enemy>();

		Debug.Log("Splash Hitting");

		if(enemyTarget != null){
			projectile.AddSplashTarget(enemyTarget);
		}

	}
}
