using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;

public static class EnemyDropImporter {

    public static Dictionary<string, EnemyDropList> deserializeEnemyDrops()
    {
        Dictionary<string, EnemyDropList> dropList = new Dictionary<string, EnemyDropList>();

        XmlSerializer serial = new XmlSerializer(typeof(EnemyDropModel));
        TextAsset file = (TextAsset)Resources.Load("xml/enemyDrop");
        StringReader stringReader = new StringReader(file.text);
        EnemyDropModel enemyDropContainer = (EnemyDropModel)serial.Deserialize(stringReader);
        stringReader.Close();

        foreach (EnemyDropList list in enemyDropContainer.DropList)
        {
            dropList.Add(list.EnemyId, list);
        }

        return dropList;
    }
}
