using UnityEngine;
using System.Collections;

public class Offering : TowerModifier {

    public float durration;

    public float fireConversionRate;
    public float coldConversionRate;
    public float earthConversionRate;

    public Offering(Modifier model) :base(model)
    {
        modifierType = ModifierTypes.Offering;
        durration = model.Duration;

        fireConversionRate = model.Properties.FireConversionRate;
        earthConversionRate = model.Properties.EarthConversionRate;
        coldConversionRate = model.Properties.ColdConversionRate;
    }
}
