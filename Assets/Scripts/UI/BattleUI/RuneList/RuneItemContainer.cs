using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RuneItemContainer : MonoBehaviour {

    public RuneListItem runeItemTemplate;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void populateWithRunes(Gem[] gems)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        rectTransform.sizeDelta = new Vector2(63 * gems.Length, 70);

        for (int i = 0; i < gems.Length; i++)
        {
            RuneListItem item = Instantiate(runeItemTemplate) as RuneListItem;
            item.setRuneImage(gems[i].gemName);
            item.GetComponent<RectTransform>().SetParent(this.transform);
            item.GetComponent<RectTransform>().anchoredPosition = new Vector2(5 + (62.5f * i), -5);
        }

    }
}
