using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour {

	public GemSlot[] gemSlots;
	public Projectile projectilePrefab;
	public Turret turret;

	public int cost;

	public SphereCollider targetRadiusArea;
	public GameObject projectileSpawnLocation;
	public float targetRadiusBase;
	public float clipMaxSizeBase;
	public float clipDelayTimeBase;
	public float reloadTimeBase;
	public float numberOfTargetsBase;

    public MeshRenderer towerRenderer;

	private float clipMaxSizeModifier;
	private float clipDelayTimeModifier;
	private float reloadTimeModifier;
	private float numberOfTargetsModifier;
	private float targetRadiusMultiplier;
	
	public List<Enemy> targetableEnemies;

	private float lastFireTime = 0;
	private float currentClipCount;
    private int gemSlotIndex = 0;

    private Offering appliedOffering;
    private float offeringEndTime;

	// Use this for initialization
	void Start () {

		ApplyGemTowerModifiers();

		targetableEnemies = new List<Enemy>(); 
		currentClipCount = clipMaxSizeBase + clipMaxSizeModifier;
	}
	
	// Update is called once per frame
	void Update () {
		if(targetableEnemies.Count > 0){

			for(int index = targetableEnemies.Count -1 ; index >=0 ; index--){

				float distance = 0;

				if(targetableEnemies[index] != null){
					distance = Vector3.Distance(transform.position, targetableEnemies[index].gameObject.transform.position);
				} else {
					RemoveTarget(targetableEnemies[index]);
				}

				//Debug.Log("Distance " + distance);
				//Debug.Log("Radius " + targetRadius.radius);

				if(distance > (targetRadiusBase * (targetRadiusMultiplier +1)) + 0.5 ){
					RemoveTarget(targetableEnemies[index]);
				}
			}
		}
	}

	void FixedUpdate(){
		if(targetableEnemies.Count > 0){

			turret.AssignTarget(targetableEnemies[0]);

			if (currentClipCount > 0){

				// Don't let this number get carried away, allow for a minimum of 0.01 seconds between shots
				float clipDelay = Mathf.Max(clipDelayTimeBase + clipDelayTimeModifier, 0.03f);

				if(lastFireTime + (clipDelay) < Time.time ){
					FireProjectile();
				}
			}else {

				// Don't let this number get carried away, allow for a minimum of 0.01 seconds between shots
				float reloadTime = Mathf.Max(reloadTimeBase + reloadTimeModifier, 0.03f);

				if(lastFireTime + (reloadTime) < Time.time){

					currentClipCount = clipMaxSizeBase + clipMaxSizeModifier;
					FireProjectile();
				}
			}

		} else if(turret.currentTarget){
			turret.RemoveTarget();
		}

        if (appliedOffering != null && offeringEndTime < Time.time)
        {
            EndOfferingDuration();
        }
	}

    public void AddRune(Gem gem)
    {
        if (gemSlotIndex <= gemSlots.Length)
        {
            Debug.Log("Tower get's rune: " + gem.itemName);

            gemSlots[gemSlotIndex].SetGem(gem);
            gemSlotIndex++;
        }
        
    }

    public void SetOffering(Offering offering)
    {
        Debug.Log("Tower get's offering: " + offering.itemName);

        appliedOffering = offering;
        StartOfferingDuration();
        
    }

    private void StartOfferingDuration()
    {
        towerRenderer.material.color = Color.green;

        clipMaxSizeModifier += appliedOffering.clipSizeModifier;
        clipDelayTimeModifier += appliedOffering.clipDelayTimeModifier;
        reloadTimeModifier += appliedOffering.reloadRateModifier;
        numberOfTargetsModifier += appliedOffering.numberOfTargetsModifier;
        targetRadiusMultiplier += appliedOffering.targetRadiusMultiplier;

        targetRadiusArea.radius = targetRadiusBase * (targetRadiusMultiplier +1);

        offeringEndTime = Time.time + appliedOffering.durration;
    }

    private void EndOfferingDuration()
    {
        towerRenderer.material.color = Color.gray;

        clipMaxSizeModifier -= appliedOffering.clipSizeModifier;
        clipDelayTimeModifier -= appliedOffering.clipDelayTimeModifier;
        reloadTimeModifier -= appliedOffering.reloadRateModifier;
        numberOfTargetsModifier -= appliedOffering.numberOfTargetsModifier;
        targetRadiusMultiplier -= appliedOffering.targetRadiusMultiplier;

        targetRadiusArea.radius = targetRadiusBase * (targetRadiusMultiplier +1);

        appliedOffering = null;
    }

    public void AddPotentailTarget(Enemy enemy){

		targetableEnemies.Add(enemy);
	}

	private void RemoveTarget(Enemy enemy){
		targetableEnemies.Remove(enemy);
	}

	private void ResetTowerModifiers(){
		clipMaxSizeModifier = 0;
		clipDelayTimeModifier = 0;
		reloadTimeModifier = 0;
		numberOfTargetsModifier = 0;
		targetRadiusMultiplier = 1;

		targetRadiusArea.radius = targetRadiusBase;
	}

	public void GemChanged(){
		ApplyGemTowerModifiers();
	}

	public void ApplyGemTowerModifiers(){

		ResetTowerModifiers();

		foreach(GemSlot gemSlot in gemSlots){

			if(gemSlot.gem != null){

				clipMaxSizeModifier += gemSlot.gem.clipSizeModifier;
				clipDelayTimeModifier += gemSlot.gem.clipDelayTimeModifier;
				reloadTimeModifier += gemSlot.gem.reloadRateModifier;
				numberOfTargetsModifier += gemSlot.gem.numberOfTargetsModifier;
				targetRadiusMultiplier += gemSlot.gem.targetRadiusMultiplier;
			}
		}

        targetRadiusArea.radius = targetRadiusBase * (targetRadiusMultiplier +1);
	}

	public void ApplyProjectileModifiers(Projectile projectile){

		Gem[] gems = new Gem[3];

		for(int index = 0 ; index < gemSlots.Length ; index++){

			if(gemSlots[index].gem != null){
				gems[index] = gemSlots[index].gem;
			}
		}

		projectile.UpdateMultipliersForGems(gems);

        if(appliedOffering != null)
        {
            projectile.UpdateMultipliersForOffering(appliedOffering);
        }
        
	}

	private void FireProjectile(){

		for(int targetIndex = 0 ; (targetIndex < targetableEnemies.Count) && (targetIndex < (numberOfTargetsBase + numberOfTargetsModifier)); targetIndex++ ){

            float distance = Vector3.Distance(transform.position, targetableEnemies[targetIndex].gameObject.transform.position);

            Debug.Log("Distance: " + distance + "Radius: " + (targetRadiusBase * (targetRadiusMultiplier +1)));

            Projectile projectile = Instantiate(projectilePrefab, projectileSpawnLocation.transform.position, Quaternion.identity) as Projectile;
			projectile.SetTarget(targetableEnemies[targetIndex]);
			ApplyProjectileModifiers(projectile);
		}

		lastFireTime = Time.time;
		currentClipCount --;
	}
}
