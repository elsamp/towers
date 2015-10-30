using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public LifeBar lifeBar;
	public GameObject body;

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

		destinationNode = EnemyPath.Instance.getNextNode(pathNodeIndex);

		if(destinationNode == null)
		{
			LevelController.Instance.EnemyReachedTown(this);
			Destroy(this.gameObject);
			return;
		}

		destination = destinationNode.transform.position;
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

        Debug.Log("Enemy TARGET hit with damage: " + totalDamage);

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

            Debug.Log("Slow Mulitiplier: " + slowMultiplier + "normalized damage: " + normalizedDamage);
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

            Debug.Log("Poison Mulitiplier: " + poisonMultiplier + "normalized damage: " + normalizedDamage);
        }
    }

    private void TakeBurnDamage(float damage)
    {
        currentLife -= damage;

        Debug.Log("Taking Burn Damage: " + damage);

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
		Destroy(this.gameObject);
	}
}
