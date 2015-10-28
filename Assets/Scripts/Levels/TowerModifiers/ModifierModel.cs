using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("towerModifiers")]
public class ModifierModel
{
    [XmlArray("modifierList"), XmlArrayItem("modifier")]
    public List<Modifier> ModifierList { get; set; }
}

public class Modifier
{
    [XmlAttribute("itemID")]
    public string ItemID { get; set; }

    [XmlAttribute("itemName")]
    public string ItemName { get; set; }

    [XmlAttribute("itemCost")]
    public int ItemCost { get; set; }

    [XmlAttribute("spriteName")]
    public string SpriteName { get; set; }

    [XmlElement("modifierProperties")]
    public ModifierProperties Properties { get; set; }

    [XmlElement("duration")]
    public int Duration { get; set; }
}

public class ModifierProperties
{
    [XmlAttribute("clipSizeModifier")]
    public float ClipSizeModifier { get; set; }

    [XmlAttribute("clipDelayTimeModifie")]
    public float ClipDelayTimeModifie { get; set; }

    [XmlAttribute("reloadRateModifier")]
    public float ReloadRateModifier { get; set; }

    [XmlAttribute("numberOfTargetsModifier")]
    public int NumberOfTargetsModifier { get; set; }

    [XmlAttribute("targetRadiusMultiplier")]
    public float TargetRadiusMultiplier { get; set; }

    [XmlAttribute("hitSplashMultiplier")]
    public float HitSplashMultiplier { get; set; }

    [XmlAttribute("hitPhysicalMultiplier")]
    public float HitPhysicalMultiplier { get; set; }

    [XmlAttribute("splashRadiusModifier")]
    public float SplashRadiusModifier { get; set; }

    [XmlAttribute("statusEffectChanceMultiplier")]
    public float StatusEffectChanceMultiplier { get; set; }

    [XmlAttribute("statusEffectDurationModifier")]
    public float StatusEffectDurationModifier { get; set; }

    [XmlAttribute("coldConversionRate")]
    public float ColdConversionRate { get; set; }

    [XmlAttribute("fireConversionRate")]
    public float FireConversionRate { get; set; }

    [XmlAttribute("earthConversionRate")]
    public float EarthConversionRate { get; set; }

    [XmlAttribute("baseStatusEffectChance")]
    public float BaseStatusEffectChance { get; set; }

}


