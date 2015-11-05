using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour {

    enum DamageElements { None, Cold, Fire, Earth }

	public SphereCollider hitRadiusArea;
	public SphereCollider splashRadiusArea;
	public GameObject bulletGO; //temp

	public float splashRadius;

	public float hitBaseFireDamage;
	public float hitBaseColdDamage;
	public float hitBaseEarthDamage;
	public float hitBasePhysicalDamage;
    public float statusEffectDuration;
    public float speed;

	private Enemy targetEnemy;
	public List<Enemy> splashTargetEnemies;

	private float hitSplashMultiplier = 1;
	private float hitGlobalMultiplier = 1;
	private float hitFireMultiplier = 1;
	private float hitColdMultiplier = 1;
	private float hitEarthMultiplier = 1;
	private float hitPhysicalMultiplier = 1;

    private float coldConversionRate = 0;
    private float fireConversionRate = 0;
    private float earthConversionRate = 0;

    private float globalStatusChance = 0.3f;

    private float splashRadiusModifier = 0;
	private float splashDamageMitigator = 0.2f;

	private Vector3 direction;
	private Vector3 splashScale;
	private bool targetHit = false;

	// Use this for initialization
	void Start () {
	
		splashScale = new Vector3(0.2f,0.2f,0.2f);
	}
	
	// Update is called once per frame
	void LateUpdate () {

		if(targetEnemy != null && !targetHit){

			direction = targetEnemy.transform.position - transform.position;
			transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

			if(HaveArrived()){
				LandHit();
			}

		} else if (targetHit){

			//splashRadiusArea.radius += splashScale.x;
			splashRadiusArea.gameObject.transform.localScale += splashScale;

			//Debug.Log("Radius: " + splashRadius + " Modifier: " + splashRadiusModifier);

			if(splashRadiusArea.gameObject.transform.localScale.x >= (splashRadius + splashRadiusModifier)){
				ApplySplashDamage();
				Destroy(this.gameObject);
			}
		} else if (targetEnemy == null){
			Destroy(this.gameObject);
		}
	}

	public void UpdateMultipliersForGems(Gem[] gems){

        // This is only called once when the projectile is created so we don't have to reset the values when we start

        foreach (Gem gem in gems)
        {
            if (gem != null)
            {
                hitSplashMultiplier += gem.hitSplashMultiplier;
                hitPhysicalMultiplier += gem.hitPhysicalMultiplier;
                splashRadiusModifier += gem.splashRadiusModifier;
            }
        }

        //Debug.Log("Elemental conversion rates| Cold: " + coldConversionRate + " Fire: " + fireConversionRate + " Earth: " + earthConversionRate);

    }

    public void UpdateMultipliersForOffering(Offering offering)
    {
        hitSplashMultiplier += offering.hitSplashMultiplier;
        hitPhysicalMultiplier += offering.hitPhysicalMultiplier;
        splashRadiusModifier += offering.splashRadiusModifier;
        
        fireConversionRate += offering.fireConversionRate;
        earthConversionRate += offering.earthConversionRate;
        coldConversionRate += offering.coldConversionRate;

        statusEffectDuration += offering.statuseffectDuration;
        globalStatusChance += offering.statusEffectChance;
        
    }

	private bool HaveArrived(){

		if(Vector3.Distance(transform.position, targetEnemy.transform.position) <= 0.03f)
		{      
			return true;
		}
		
		return false;
	}

	private void LandHit(){

		//Debug.Log("Target Hit!");
		ApplyTargetDamage();

		targetHit = true;
	}

	public void SetTarget(Enemy enemy){

		//Debug.Log("Set Target");
		targetEnemy = enemy;
	}

	public void AddSplashTarget(Enemy enemy){

		if(enemy != targetEnemy && !splashTargetEnemies.Contains(enemy)){
			splashTargetEnemies.Add(enemy);
		}
	}

	private float ColdDamage(){

        return (hitBaseColdDamage + ElementConvertedAmount(coldConversionRate)) * hitColdMultiplier;
	}

	private float FireDamage(){

		return (hitBaseFireDamage + ElementConvertedAmount(fireConversionRate)) * hitFireMultiplier;
	}

	private float EarthDamage(){

		return (hitBaseEarthDamage + ElementConvertedAmount(earthConversionRate)) * hitEarthMultiplier;
	}

    private float ElementConvertedAmount(float elementalConvertionRate)
    {
        float convertedElementAmount = 0;

        if (elementalConvertionRate > 0)
        {
            convertedElementAmount = PhysicalDamage() * elementalConvertionRate;
        }

        //Debug.Log("Coverted elemental amount: " + convertedElementAmount);

        return convertedElementAmount;
    }

    private float ConvertedPhysicalDamage()
    {
        return PhysicalDamage() - (ElementConvertedAmount(coldConversionRate) + ElementConvertedAmount(fireConversionRate) + ElementConvertedAmount(earthConversionRate));
    }

	private float PhysicalDamage(){

        return hitBasePhysicalDamage * (hitPhysicalMultiplier +1);
    }

	private void ApplyTargetDamage(){
		targetEnemy.TakeDamage(ConvertedPhysicalDamage(), ColdDamage(), FireDamage(),EarthDamage(), globalStatusChance,statusEffectDuration);

        Debug.Log("Enemy TARGET hit with damage| Physical: " + ConvertedPhysicalDamage() + 
            " Cold: " + ColdDamage() + " Fire: " + FireDamage() + " Earth: " + EarthDamage());
	}

    private void ApplySplashDamage(){

        float physicalSlpash = ConvertedPhysicalDamage() * splashDamageMitigator * (hitSplashMultiplier + 1);
        float coldSplash = ColdDamage() * splashDamageMitigator * (hitSplashMultiplier + 1);
        float fireSplash = FireDamage() * splashDamageMitigator * (hitSplashMultiplier + 1);
        float earthSplash = EarthDamage() * splashDamageMitigator * (hitSplashMultiplier + 1);

        foreach (Enemy enemy in splashTargetEnemies){
			if(enemy != null){

				enemy.TakeDamage(physicalSlpash,coldSplash,fireSplash,earthSplash, globalStatusChance,statusEffectDuration);

                /*Debug.Log("Enemy SPLASH hit with damage| Physical: " + physicalSlpash +
            " Cold: " + coldSplash + " Fire: " + fireSplash + " Earth: " + earthSplash);*/
            }
		}
	}
}
