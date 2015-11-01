using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlBalanceBar : MonoBehaviour {

    private int maxControl = 1;
    private float currentControl = 1;

    public Image backgroundBar;
    public Image frontBar;

    private float maxBarWidth;
    private float barHeight;

    // Use this for initialization
    void Start () {

        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Init()
    {
        maxControl = LevelController.Instance.maxControl;
        currentControl = LevelController.Instance.startingControl;

        Debug.Log("starting control:" + currentControl);

        maxBarWidth = backgroundBar.rectTransform.sizeDelta.x;
        barHeight = backgroundBar.rectTransform.sizeDelta.y;

        UpdateControlBar(currentControl);
    }

    public void UpdateControlBar(float control)
    {
        currentControl = control;

        if (currentControl <= 0)
        {
            LevelController.Instance.EndLevel(false);
            Debug.Log("------ GAME OVER! ------");

        } else if (!(currentControl > maxControl))
        {
            float multiplier = currentControl / maxControl;
            frontBar.rectTransform.sizeDelta = new Vector2(maxBarWidth * multiplier, barHeight);
        }
   
    }

}
