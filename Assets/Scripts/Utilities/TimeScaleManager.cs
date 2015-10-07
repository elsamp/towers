using UnityEngine;
using System.Collections;

public class TimeScaleManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyUp(KeyCode.Keypad1))
        {
            Time.timeScale = 1;
        }else if(Input.GetKeyUp(KeyCode.Keypad2))
        {
            Time.timeScale = 2;
        }
        else if (Input.GetKeyUp(KeyCode.Keypad3))
        {
            Time.timeScale = 3;
        }

    }
}
