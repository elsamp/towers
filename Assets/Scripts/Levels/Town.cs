using UnityEngine;
using System.Collections;

public class Town : MonoBehaviour {

	public float maxLife;
	public LifeBar lifeBar;

	private float currentLife;

	// Use this for initialization
	void Start () {
		currentLife = maxLife;
		lifeBar.Init(maxLife);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TakeHit(float hitDamage){

		currentLife -= hitDamage;

		if(currentLife > 0){
			lifeBar.UpdateLifeBar(currentLife);
		} else {
			LevelController.Instance.EndLevel(false);
			Debug.Log("------ GAME OVER! ------");
		}

	}

	public void Heal(float healAmount){

		currentLife += healAmount;

		if(currentLife > maxLife){
			currentLife = maxLife;
		}

		lifeBar.UpdateLifeBar(currentLife);
	}
}
