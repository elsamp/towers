using UnityEngine;
using System.Collections;

public class Ink {

    public enum InkElement {Cold, Fire, Earth, None};

    public InkElement inkElement;

    public float coldConversion;
    public float fireConversion;
    public float earthConversion;

    public float elementalEffectChance;


    public Ink(float coldConversion, float fireConversion, float earthConversion, float elementalChance, InkElement element)
    {
        this.coldConversion = coldConversion;
        this.fireConversion = fireConversion;
        this.earthConversion = earthConversion;

        elementalEffectChance = elementalChance;
        inkElement = element;
    }

}
