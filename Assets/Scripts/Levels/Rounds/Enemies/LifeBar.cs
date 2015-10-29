using UnityEngine;
using System.Collections;

public class LifeBar : MonoBehaviour {

	public GameObject maxLifeSprite;
	public GameObject currentLifeSprite;

	private float maxLife;
	private float maxWidth;

	// Use this for initialization
	void Start () {
	
	}

	public void Init(float maxLife){
		this.maxLife = maxLife;
		maxWidth = maxLifeSprite.transform.localScale.x;

		UpdateLifeBar(maxLife);
	}
	
	public void UpdateLifeBar(float currentLife){

		float multiplier = maxWidth / maxLife;

		Vector3 scale = currentLifeSprite.transform.localScale;
		scale.x = currentLife * multiplier;

		currentLifeSprite.transform.localScale = scale;
	}
}
