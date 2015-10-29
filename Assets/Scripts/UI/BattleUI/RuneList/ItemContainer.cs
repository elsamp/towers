using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemContainer : MonoBehaviour {

    // again make parent class ListItem and dirrive from it
    public ListItem itemTemplatePrefab;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void show()
    {
        this.gameObject.SetActive(true);
    }

    public void hide()
    {
        this.gameObject.SetActive(false);
    }

    public void populateWithItems(TowerModifier[] modifiers)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        rectTransform.sizeDelta = new Vector2(63 * modifiers.Length, 70);

        for (int i = 0; i < modifiers.Length; i++)
        {
            Debug.Log("A MODIFIER");
            ListItem item = Instantiate(itemTemplatePrefab) as ListItem;
            item.setItemImage(modifiers[i].itemName);
            item.modifierObject = modifiers[i];
            item.GetComponent<RectTransform>().SetParent(this.transform);
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(5 + (62.5f * i), -5);
        }

    }
}
