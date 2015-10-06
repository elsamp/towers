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
		
		transform.Translate(direction.normalized * (speed * 0.1f) * Time.deltaTime, Space.World);

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

	public void TakeDamage(float physicalDamage, float iceDamage, float fireDamage, float earthDamage){

		float totalDamage = 0;

		totalDamage += Mathf.Max(physicalDamage - physicalResist, 0);
		totalDamage += Mathf.Max(iceDamage - coldResist, 0);
		totalDamage += Mathf.Max(fireDamage - fireResist, 0);
		totalDamage += Mathf.Max(earthDamage - earthResist, 0);

		Debug.Log ("Taking Damage:" + totalDamage);

		currentLife -= totalDamage;

		if(currentLife <= 0){
			Die();
		} else {
			lifeBar.UpdateLifeBar(currentLife);
		}

	}

	private void Die(){
		LevelController.Instance.EnemyKilled(this);
		Destroy(this.gameObject);
	}
}
