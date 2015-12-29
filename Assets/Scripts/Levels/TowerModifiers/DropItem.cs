using UnityEngine;
using System.Collections;

public class DropItem : MonoBehaviour {

    private Offering item;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseUp()
    {
        Debug.Log("DropItem : OnMouseUp --> Picked up item " + item.itemName);
        // Add item to inventory
        Destroy(this.gameObject);
    }

    public void SetDropItem(Offering dropItem)
    {
        item = dropItem;
    }
}
