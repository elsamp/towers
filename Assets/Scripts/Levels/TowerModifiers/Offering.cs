using UnityEngine;
using System.Collections;

public class Offering : TowerModifier {

    public float durration;

	public Offering(Modifier model) :base(model)
    {
        modifierType = ModifierTypes.Offering;
        durration = model.Duration;
    }
}
