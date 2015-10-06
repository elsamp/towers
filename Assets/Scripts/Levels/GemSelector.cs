using UnityEngine;
using System.Collections;

public class GemSelector : MonoBehaviour {

	public Gem[] availibleGems;
	public bool isActive;

	private GemSlot selectedSlot;

	private static GemSelector instance;

	public static GemSelector Instance
	{
		get{return instance;}
	}

	public void Init () {
		
		if(instance == null){
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){

		if(isActive){

			int noneButtonIndex = 0;

			for (int index = 0 ; index < availibleGems.Length ; index++){
				if (GUI.Button(new Rect(10, 10 + (30 * index), 150, 20), availibleGems[index].gemName)){
					selectedSlot.SetGem(availibleGems[index]); 
					isActive = false;
				}

				noneButtonIndex = index + 1;
			}

			if (GUI.Button(new Rect(10, 10 + (30 * noneButtonIndex), 150, 20), "None")){
				selectedSlot.SetGem(null); 
				isActive = false;
			}
		}
	}

	public void SetSelectedSlot(GemSlot slot){
		selectedSlot = slot;
	}
}
