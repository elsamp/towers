﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	public LifeBar lifeBar;
	public GameObject body;

    public string enemyId;

	public float maxLife;
	public float currentLife;
	public float speed;

	public float fireResist;
	public float coldResist;
	public float earthResist;
	public float physicalResist;
	public float allResist;

	public float killRewardAmount;
	public float townDamageAmount;

	private int pathNodeIndex;
	private EnemyPathNode destinationNode;
	private Vector3 destination;
	private Vector3 direction;

    private float fireStatusEndTime = 0;
    private float coldStatusEndTime = 0;
    private float earthStatusEndTime = 0;

    private float nextBurnHitTime = 0;
    private float burnDamage = 0;
    private float slowMultiplier = 1;
    private float poisonMultiplier = 1;

    private GameObject fireStatusEffect;
    private GameObject coldStatusEffect;
    private GameObject earthStatusEffect;

    private EnemyPath path;
    private Round round;

    public EnemyPath Path
    {
        set { path = value; }
    }

    public Round Round
    {
        set { round = value; }
    }


    // Use this for initialization
    void Start () {
		lifeBar.Init(maxLife);
		pathNodeIndex = 0;
		UpdateDestination();
		currentLife = maxLife;

	}
	
	// Update is called once per frame
	void LateUpdate () {
	
		if(HaveArrived()){
			UpdateDestination();
		}
		direction = destination - transform.position;
        //body.transform.LookAt(destinationNode.transform.position);

        transform.Translate(direction.normalized * EffectedSpeed() * Time.deltaTime, Space.World);
        
        UpdateForStatusEffect();

	}

    private float EffectedSpeed()
    {
        float effectedSpeed;

        if (ColdEffectIsActive())
        {

            effectedSpeed = ((speed * slowMultiplier) * 0.1f);
        }
        else
        {
            effectedSpeed = (speed * 0.1f);
        }

        return effectedSpeed;
    }

	public bool HaveArrived(){

		if(Vector3.Distance(transform.position, destination) <= 0.03)
		{      
			return true;
		}

		return false;
	}

	public void UpdateDestination(){

		destinationNode = path.getNextNode(pathNodeIndex);

		if(destinationNode == null)
		{
			LevelController.Instance.EnemyReachedTown(this);
            round.decreaseActiveEnemies();
            Destroy(this.gameObject);
			return;
		}

        float randomX = Random.Range(0.0f, 0.2f);
        float randomZ = Random.Range(0.0f, 0.2f);

        Vector3 pos = destinationNode.transform.position;

        pos.x += randomX;
        pos.z += randomZ;

        destination = pos;
		pathNodeIndex++;

	}

    public void TakeDamage(float physicalDamage, float coldDamage, float fireDamage, float earthDamage, float statusChance, float statusDuration) {

        float totalDamage = 0;

        if (physicalDamage > 0)
        { 
            totalDamage += Mathf.Max(physicalDamage - (physicalDamage * physicalResist), 0);
        }

        if (coldDamage > 0)
        {
            totalDamage += Mathf.Max(coldDamage - (coldDamage * coldResist), 0);

            if (RollDiceForStatusEffect(statusChance))
            {
                AddColdStatus(statusDuration);
            }
        }

        if (fireDamage > 0)
        {
            totalDamage += Mathf.Max(fireDamage - (fireDamage * fireResist), 0);

            if (RollDiceForStatusEffect(statusChance))
            {
                AddFireStatus(statusDuration);
            }
        }

        if (earthDamage > 0)
        {
            totalDamage += Mathf.Max(earthDamage - (earthDamage * earthResist), 0);

            if (RollDiceForStatusEffect(statusChance))
            {
                AddEarthStatus(statusDuration);
            }
        }

        totalDamage /= poisonMultiplier;

		currentLife -= totalDamage;

        //Debug.Log("Enemy TARGET hit with damage: " + totalDamage);

        CheckForDeath();

        CalculateBurnDamage(fireDamage);
        CalculateSlowMultiplier(coldDamage);
        CalculatePoisionMultiplier(earthDamage);
    }

    private void CalculateBurnDamage(float fireDamage)
    {
        if (fireDamage > 0)
        {
            burnDamage = fireDamage * 0.5f;
        }
    }

    private void CalculateSlowMultiplier(float coldDamage)
    {
        if (coldDamage > 0)
        {
            if (coldDamage > 10)
            {
                coldDamage = 10;
            }

            float normalizedDamage = coldDamage / 10;
            slowMultiplier = 1 - normalizedDamage;

            //Debug.Log("Slow Mulitiplier: " + slowMultiplier + "normalized damage: " + normalizedDamage);
        }
    }

    private void CalculatePoisionMultiplier(float earthDamage)
    {
        if (earthDamage > 0)
        {
            if (earthDamage > 10)
            {
                earthDamage = 10;
            }

            float normalizedDamage = earthDamage / 10;
            poisonMultiplier = 1 - normalizedDamage;

            //Debug.Log("Poison Mulitiplier: " + poisonMultiplier + "normalized damage: " + normalizedDamage);
        }
    }

    private void TakeBurnDamage(float damage)
    {
        currentLife -= damage;

        //Debug.Log("Taking Burn Damage: " + damage);

        CheckForDeath();

        nextBurnHitTime = Time.time + 0.5f;
    }

    private void UpdateForStatusEffect()
    {
        if(fireStatusEffect != null)
        {
            if (FireEffectIsActive())
            {
                if (nextBurnHitTime < Time.time)
                {
                    TakeBurnDamage(burnDamage);
                }

            } else
            {
                TakeBurnDamage(burnDamage);
                RemoveStatusEffect(fireStatusEffect);
                burnDamage = 0;
            } 
        }

        if (coldStatusEffect != null && !ColdEffectIsActive())
        {
            RemoveStatusEffect(coldStatusEffect);
            slowMultiplier = 1;
        }

        if (earthStatusEffect != null && !EarthEffectIsActive())
        {
            RemoveStatusEffect(earthStatusEffect);
            poisonMultiplier = 1;
        }
    }

    private bool ColdEffectIsActive()
    {
        if(Time.time < coldStatusEndTime)
        {
            return true;
        }

        return false;
    }

    private bool FireEffectIsActive()
    {
        if (Time.time < fireStatusEndTime)
        {
            return true;
        }

        return false;
    }

    private bool EarthEffectIsActive()
    {
        if (Time.time < earthStatusEndTime)
        {
            return true;
        }

        return false;
    }

    private void AddColdStatus(float duration)
    {
        coldStatusEndTime = Time.time + duration;
        if (coldStatusEffect == null)
        {
            coldStatusEffect = Instantiate(LevelController.Instance.coldStatusEffectPrefab, this.transform.position, Quaternion.identity) as GameObject;
            coldStatusEffect.transform.parent = this.gameObject.transform;
        }
    }

    private void AddFireStatus(float duration)
    {
        fireStatusEndTime = Time.time + duration;
        if (fireStatusEffect == null && this.gameObject != null)
        {
            fireStatusEffect = Instantiate(LevelController.Instance.fireStatusEffectPrefab, this.gameObject.transform.position, this.gameObject.transform.rotation) as GameObject;
            fireStatusEffect.transform.parent = this.gameObject.transform;

            TakeBurnDamage(burnDamage);
        }
    }

    private void AddEarthStatus(float duration)
    {
        earthStatusEndTime = Time.time + duration;

        if(earthStatusEffect == null)
        {
            earthStatusEffect = Instantiate(LevelController.Instance.earthStatusEffectPrefab, this.transform.position, Quaternion.identity) as GameObject;
            earthStatusEffect.transform.parent = this.gameObject.transform;

        }
        
    }

    private void RemoveStatusEffect(GameObject statusEffect)
    {
        Destroy(statusEffect);
    }

    public bool RollDiceForStatusEffect(float chance)
    {
        float roll = Random.Range(0.0f, 1.0f);

        if(roll < chance)
        {
            return true;
        }

        return false;
    }

    private void CheckForDeath()
    {
        if (currentLife <= 0)
        {
            Die();
        }
        else
        {
            lifeBar.UpdateLifeBar(currentLife);
        }
    }

	private void Die(){
		LevelController.Instance.EnemyKilled(this);
        DropItem();
        round.decreaseActiveEnemies();
		Destroy(this.gameObject);
	}

    private void DropItem()
    {
        Debug.Log("Rolling for Drop Item");

        Item dropItem = RollForDrop();

        if (dropItem != null)
        {
            Debug.Log("Dropping Item: " + dropItem.ItemID);
            SpawnDropItem(EnemyDropManager.Instance.GetOfferingForId(dropItem.ItemID));
        }

    }

    private void SpawnDropItem(Offering dropItem)
    {
        Vector3 dropPosition = this.transform.position;
        dropPosition.y = 0.25f;
        Vector3 rotation = new Vector3(45,0,0);

        DropItem item = Instantiate(LevelController.Instance.dropItemPrefab, dropPosition, Quaternion.Euler(rotation)) as DropItem;
        item.SetDropItem(dropItem);
    }

    private Item RollForDrop()
    {

        List <Item> potentialDrops = EnemyDropManager.Instance.GetEnemyDropsForRegion(enemyId);

        Debug.Log("Potential Items: " + potentialDrops.Count);

        Dictionary<float, Item> chanceList = new Dictionary<float, Item>();
        float[] chanceTimes = new float[potentialDrops.Count];

        float chanceMark = 0;

        for (int i = 0; i < potentialDrops.Count; i++)
        {
            chanceMark += potentialDrops[i].DropChance;
            chanceList.Add(chanceMark, potentialDrops[i]);
            chanceTimes[i] = chanceMark;
        }

        float roll = Random.Range(0.0f, 1.0f);

        for (int i = 0; i < chanceTimes.Length; i++)
        {
            if (roll < chanceTimes[i])
            {
                return chanceList[chanceTimes[i]];
            }
        }

        return null;
    }
}
