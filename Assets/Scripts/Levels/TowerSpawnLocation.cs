using UnityEngine;
using System.Collections;

public class TowerSpawnLocation : MonoBehaviour {

	public Tower towerPrefab;

    private Vector3 mouseDownPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
    {
        mouseDownPos = Input.mousePosition;
		
		
	}

    void OnMouseUp()
    {
        if(AreClose(mouseDownPos, Input.mousePosition))
        {
            if (LevelController.Instance.SpendFavour(towerPrefab.cost))
            {
                Instantiate(towerPrefab, this.gameObject.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }

    private bool AreClose(Vector3 pos1, Vector3 pos2)
    {
        if((Mathf.Abs(pos1.x - pos2.x) < 5) && (Mathf.Abs(pos1.y - pos2.y) < 5))
        {
            return true;
        }

        return false;
    }
}


