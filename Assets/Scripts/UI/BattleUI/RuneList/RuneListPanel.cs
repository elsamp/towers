using UnityEngine;
using System.Collections;

public class RuneListPanel : MonoBehaviour {

    public RuneItemContainer itemContainer;
    public Gem[] availibleRunes;

    // Use this for initialization
    void Start () {
        populateContainerWithRunes(availibleRunes);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void populateContainerWithRunes(Gem[] gemsToPopulate)
    {
        itemContainer.populateWithRunes(gemsToPopulate);
    }
}
