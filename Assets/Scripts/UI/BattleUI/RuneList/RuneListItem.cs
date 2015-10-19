using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RuneListItem : MonoBehaviour {

    public Image runeImage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setRuneImage(string resourceName)
    {
        this.GetComponentInChildren<Text>().text = resourceName;
    }
}
