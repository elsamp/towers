using UnityEngine;
using System.Collections;

public class Ink {

    public enum InkElement {Cold, Fire, Earth, None};

    public InkElement inkElement;

    public float coldConversion = 0;
    public float fireConversion = 0;
    public float earthConversion = 0;

    public float elementalEffectChance = 0;                 // number from 0-1


    public Ink(float coldConversion, float fireConversion, float earthConversion, float elementalChance, InkElement element)
    {
        this.coldConversion = coldConversion;
        this.fireConversion = fireConversion;
        this.earthConversion = earthConversion;

        elementalEffectChance = elementalChance;
        inkElement = element;
    }

}
