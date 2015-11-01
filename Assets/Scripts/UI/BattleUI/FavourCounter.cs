using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FavourCounter : MonoBehaviour {

    public Text counterLabel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setTextValue(int value)
    {
        counterLabel.text = value.ToString();
    }
}
