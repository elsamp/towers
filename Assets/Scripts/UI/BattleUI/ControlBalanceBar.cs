using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlBalanceBar : MonoBehaviour {

    public int maxControl;
    public float currentControl;

    public Image backgroundBar;
    public Image frontBar;

    private float maxBarWidth;
    private float barHeight;

    // Use this for initialization
    void Start () {
        maxBarWidth = backgroundBar.rectTransform.sizeDelta.x;
        barHeight = backgroundBar.rectTransform.sizeDelta.y;

        UpdateControlBar(0);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateControlBar(float controlChange)
    {
        currentControl += controlChange;

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
