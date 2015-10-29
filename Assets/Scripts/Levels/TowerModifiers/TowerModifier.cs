﻿using UnityEngine;
using System.Collections;

public class TowerModifier {

    public enum ModifierTypes { None, Gem, Offering}

    public string itemName;
    public Color itemColour;
    public float itemCost;

    public ModifierTypes modifierType;

    //Tower Modifiers
    public float clipSizeModifier;
    public float clipDelayTimeModifier;
    public float reloadRateModifier;
    public float numberOfTargetsModifier;
    public float targetRadiusMultiplier = 1;

    // Projectile Modifiers
    public float hitSplashMultiplier = 1;
    public float hitPhysicalMultiplier = 1;
    public float splashRadiusModifier = 0;

    public float statusEffectChance = 0;
    public float statuseffectDuration = 0;


    public TowerModifier(Modifier model)
    {
        modifierType = ModifierTypes.None;

        itemName = model.ItemName;
        itemCost = model.ItemCost;

        clipSizeModifier = model.Properties.ClipSizeModifier;
        clipDelayTimeModifier = model.Properties.ClipDelayTimeModifie;
        reloadRateModifier = model.Properties.ReloadRateModifier;
        numberOfTargetsModifier = model.Properties.NumberOfTargetsModifier;
        targetRadiusMultiplier = model.Properties.TargetRadiusMultiplier;

        hitSplashMultiplier = model.Properties.HitSplashMultiplier;
        hitPhysicalMultiplier = model.Properties.HitPhysicalMultiplier;
        splashRadiusModifier = model.Properties.SplashRadiusModifier;

        statusEffectChance = model.Properties.StatusEffectChanceMultiplier;
        statuseffectDuration = model.Properties.StatusEffectDurationModifier;
    }
}
