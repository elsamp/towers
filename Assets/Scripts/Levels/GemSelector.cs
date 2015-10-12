using UnityEngine;
using System.Collections;

public class GemSelector : MonoBehaviour {

	public Gem[] availibleGems;
	public bool isActive;

	private GemSlot selectedSlot;
    private Ink ink;

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
        ink = new Ink(0, 0, 0, 0, Ink.InkElement.None);
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    // prototype hacky GUI - get rid of this as soon as possible
	void OnGUI(){

		if(isActive){

			int noneButtonIndex = 0;

			for (int index = 0 ; index < availibleGems.Length ; index++){
				if (GUI.Button(new Rect(10, 50 + (30 * index), 150, 20), availibleGems[index].gemName)){

                    availibleGems[index].ink = ink;
					selectedSlot.SetGem(availibleGems[index]); 
					isActive = false;
				}

				noneButtonIndex = index + 1;
			}

			if (GUI.Button(new Rect(10, 50 + (30 * noneButtonIndex), 150, 20), "None")){
				selectedSlot.SetGem(null); 
				isActive = false;
			}

            if (GUI.Button(new Rect(10, 10, 50, 20), "cold"))
            {
                ink = new Ink(0.3f,0,0,0.1f,Ink.InkElement.Cold);
            }

            if (GUI.Button(new Rect(70, 10, 50, 20), "earth"))
            {
                ink = new Ink(0, 0, 0.3f,0.1f, Ink.InkElement.Earth);
            }

            if (GUI.Button(new Rect(140, 10, 50, 20), "fire"))
            {
                ink = new Ink(0, 0.3f, 0, 0.1f, Ink.InkElement.Fire);
            }
        }
	}

	public void SetSelectedSlot(GemSlot slot){
		selectedSlot = slot;
	}
}
