using UnityEngine;
using System.Collections;

public class TowerSpawnLocation : MonoBehaviour {

	public Tower towerPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		
		if (LevelController.Instance.SpendFavour(towerPrefab.cost))
        {
            Instantiate(towerPrefab, this.gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
		
	}
}


