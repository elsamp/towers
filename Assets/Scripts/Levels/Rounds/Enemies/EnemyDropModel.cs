using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("dropTable")]
public class EnemyDropModel {

    [XmlArray("enemyDrops"), XmlArrayItem("enemyDropList")]
    public List<EnemyDropList> DropList { get; set; }

}

public class EnemyDropList
{
    [XmlArray("enemyDropList"), XmlArrayItem("item")]
    public List<Item> ItemList { get; set; }

    [XmlAttribute("enemyId")]
    public string EnemyId { get; set; }
}


public class Item
{
    [XmlAttribute("itemId")]
    public string ItemID { get; set; }

    [XmlAttribute("dropChance")]
    public float DropChance { get; set; }
}
