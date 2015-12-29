using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {

    public int startingControl;
    public int maxControl;
	public ControlBalanceBar balanceBar;
    public int startingFavour;
    public FavourCounter favourGUI;

    private int currentFavour;
    private float currentControl;

    public GameObject earthStatusEffectPrefab;
    public GameObject coldStatusEffectPrefab;
    public GameObject fireStatusEffectPrefab;

    public DropItem dropItemPrefab;

    private static LevelController instance;
	
	public static LevelController Instance
	{
		get {return instance;}
	}
	
	// Use this for initialization
	void Start () {
		if(instance == null){
			instance = this;
		}
		
		FindObjectOfType<RoundController>().Init();
        FindObjectOfType<EnemyDropManager>().Init();

        currentControl = startingControl;
        currentFavour = startingFavour;
        favourGUI.setTextValue(currentFavour);

        balanceBar.Init();
    }

	public bool SpendFavour(int amount){

        Debug.Log("Amount = " + amount);

        if(currentFavour >= amount)
        {
            currentFavour -= amount;
            favourGUI.setTextValue(currentFavour);
            return true;
        }

        return false;
	}

    public void AwardRoundFavour(int amount)
    {
        currentFavour += amount;
        favourGUI.setTextValue(currentFavour);
    }

    public void EnemyReachedTown(Enemy enemy){
        currentControl -= enemy.townDamageAmount;
        balanceBar.UpdateControlBar(currentControl);
	}

	public void EnemyKilled(Enemy enemy){
        currentControl += enemy.killRewardAmount;
        balanceBar.UpdateControlBar(currentControl);
    }

	public void EndLevel(bool isSuccessful){

		if(isSuccessful){
			// go on with game mechanics
		} else {
			RoundController.Instance.StopCurrentRound();
		}
	}
}
