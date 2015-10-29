using UnityEngine;
using System.Collections;

public class ListPanel : MonoBehaviour {

    public ItemContainer runeContainer;
    public ItemContainer offeringContainer;

    private Gem[] gemModifiers;
    private Offering[] offeringModifiers;

    private TowerModifier.ModifierTypes selectedModifierType;

    // Use this for initialization
    void Start () {

        gemModifiers = ModifierImporter.deserializeGems();
        offeringModifiers = ModifierImporter.deserializeOfferings();

        selectedModifierType = TowerModifier.ModifierTypes.Gem;
        showContainerForSelectedType();
        populateContainerWithItems();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void toggleModifierType()
    {
        if(selectedModifierType == TowerModifier.ModifierTypes.Gem)
        {
            selectedModifierType = TowerModifier.ModifierTypes.Offering;
        } else
        {
            selectedModifierType = TowerModifier.ModifierTypes.Gem;
        }

        showContainerForSelectedType();
    }

    private void showContainerForSelectedType()
    {
        if(selectedModifierType == TowerModifier.ModifierTypes.Gem)
        {
            runeContainer.show();
            offeringContainer.hide();

        } else
        {
            runeContainer.hide();
            offeringContainer.show();
        }
    }

    private void populateContainerWithItems()
    {
        runeContainer.populateWithItems(gemModifiers);
        offeringContainer.populateWithItems(offeringModifiers);

    }
}
