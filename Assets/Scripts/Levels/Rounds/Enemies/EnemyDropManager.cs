using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDropManager : MonoBehaviour {

    private Dictionary<string, EnemyDropList> enemyDrops;

    private static EnemyDropManager instance;

    public static EnemyDropManager Instance
    {
        get { return instance; }
    }

    public void Init()
    {

        if (instance == null)
        {
            instance = this;
            enemyDrops = EnemyDropImporter.deserializeEnemyDrops();
        }
    }

    // Use this for initialization
    void Start () {

        
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public List<Item> GetEnemyDropsForRegion(string enemyId)
    {
        List<Item> items = new List<Item>();

        EnemyDropList list;
        enemyDrops.TryGetValue(enemyId, out list);

        return items;
    }

}
