﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDropManager : MonoBehaviour {

    private Dictionary<string, EnemyDropList> enemyDrops;
    private Dictionary<string, Offering> drops;

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

            Debug.Log("Drop Manager Init");
            enemyDrops = EnemyDropImporter.deserializeEnemyDrops();
            drops = ModifierImporter.getOfferingDictionary();
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
        EnemyDropList list = new EnemyDropList();

        enemyDrops.TryGetValue(enemyId, out list);

        //Debug.Log("enemyDrops " + enemyDrops.Count);
        //Debug.Log("drops for enemy ID " + enemyId + ": " + list.ItemList.Count);

        return list.ItemList;
    }

    public Offering GetOfferingForId(string id)
    {
        if (drops.ContainsKey(id))
        {
            return drops[id];
        }

        return null;
    }

}
