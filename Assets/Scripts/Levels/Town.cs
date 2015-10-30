using UnityEngine;
using System.Collections;

public class Town : MonoBehaviour {

	public ControlBalanceBar lifeBar;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TakeHit(float hitDamage){

		lifeBar.UpdateControlBar(-hitDamage);
	}

	public void Heal(float healAmount){

		lifeBar.UpdateControlBar(healAmount);
	}
}
