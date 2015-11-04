using UnityEngine;
using System.Collections;

public class CameraPanner : MonoBehaviour {

    private bool isDragging;
    private bool isDragLocked;

    public float maxXPos;
    public float maxZPos;
    public float minXPos;
    public float minZPos;

    private Vector3 moveVector;
    private Vector3 mouseStartPos;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (isDragging)
        {

            Vector3 adjustedMove = moveVector;
            adjustedMove.x += (mouseStartPos.x - Input.mousePosition.x) * 0.01f;
            adjustedMove.z += (mouseStartPos.y - Input.mousePosition.y) * 0.01f;
            Debug.Log(moveVector.z + " || " + Input.mousePosition.y);


            if (adjustedMove.x > maxXPos)
            {
                adjustedMove.x = maxXPos;
            }

            if (adjustedMove.z > maxZPos)
            {
                adjustedMove.z = maxZPos;
            }

            if (adjustedMove.x < minXPos)
            {
                adjustedMove.x = minXPos;
            }

            if (adjustedMove.z < minZPos)
            {
                adjustedMove.z = minZPos;
            }

            this.gameObject.transform.position = adjustedMove;

        }
	}

    public void StartDragging()
    {
        if (!isDragLocked)
        {
            isDragging = true;

            mouseStartPos = Input.mousePosition;
            moveVector = this.transform.position;
        }

    }

    public void EndDragging()
    {
        isDragging = false;
    }

    public void setLockDrag(bool isLocked)
    {
        isDragLocked = isLocked;

        if (isLocked && isDragging)
        {
            EndDragging();
        }
    }

}
