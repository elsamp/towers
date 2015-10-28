using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;

public class ModifierImporter {

	private List<Modifier> deserializeModifiers(string filePath)
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

    public List<Gem> deserializeGems()
    {
        List<Gem> gems = new List<Gem>();

        foreach (Modifier mod in deserializeModifiers("xml/gemsXML"))
        {
            // create new Gem using constructer passing Modifier
            // add to gems list
        }

        return gems;
    }

    public List<Offering> deserializeOfferings()
    {
        List<Offering> offerings = new List<Offering>();

        foreach (Modifier mod in deserializeModifiers("xml/offeringsXML"))
        {
            // create new Offering using constructer passing Modifier
            // add to offering list
        }

        return offerings;
    }

}
