using UnityEngine;
using System.Collections;

public class GemSlot : MonoBehaviour {

	public Gem gem;
	public Tower tower;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetGem(Gem newGem){
		gem = newGem; 
		tower.GemChanged();

		if(gem != null){
			this.GetComponent<Renderer>().material.color = gem.gemColour;
			LevelController.Instance.SpendReputation(gem.gemCost);
		} else {
			this.GetComponent<Renderer>().material.color = Color.white;
		}
	}

	void OnMouseUp(){

		Debug.Log("Clicked");

		GemSelector.Instance.isActive = true;
		GemSelector.Instance.SetSelectedSlot(this);
	}
}
