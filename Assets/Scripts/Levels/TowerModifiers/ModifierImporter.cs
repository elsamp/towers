using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;

public static class ModifierImporter {

	private static List<Modifier> deserializeModifiers(string filePath)
    {
        List<Modifier> modifiers = new List<Modifier>();

        XmlSerializer serial = new XmlSerializer(typeof(ModifierModel));
        TextAsset file = (TextAsset)Resources.Load(filePath);
        StringReader stringReader = new StringReader(file.text);
        ModifierModel modifierContainer = (ModifierModel)serial.Deserialize(stringReader);
        stringReader.Close();

        foreach (Modifier mod in modifierContainer.ModifierList)
        {
            modifiers.Add(mod);
        }

        return modifiers;
    }

    public static Gem[] deserializeGems()
    {
        List<Gem> gems = new List<Gem>();

        foreach (Modifier mod in deserializeModifiers("xml/gemsXML"))
        {
            gems.Add(new Gem(mod));
        }

        return gems.ToArray();
    }

    public static Offering[] deserializeOfferings()
    {
        List<Offering> offerings = new List<Offering>();

        foreach (Modifier mod in deserializeModifiers("xml/offeringsXML"))
        {
            offerings.Add(new Offering(mod));
        }

        return offerings.ToArray();
    }

}
