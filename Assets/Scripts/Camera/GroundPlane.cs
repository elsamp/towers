using UnityEngine;
using System.Collections;

public class GroundPlane : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        Camera.main.GetComponent<CameraPanner>().StartDragging();

        Debug.Log("StartDragging");
    }

    void OnMouseUp()
    {
        Camera.main.GetComponent<CameraPanner>().EndDragging();
    }
}
