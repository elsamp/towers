using UnityEngine;
using System.Collections;

public class Gem : MonoBehaviour {

	public string gemName;
	public Color gemColour;
	public float gemCost;

	//Tower Modifiers
	public float clipSizeModifier;
	public float clipDelayTimeModifier;
	public float reloadRateModifier;
	public float numberOfTargetsModifier;
	public float targetRadiusMultiplier = 1;

	// Projectile Modifiers
	public float hitSplashMultiplier = 1;
	public float hitGlobalMultiplier = 1;
	public float hitFireMultiplier = 1;
	public float hitColdMultiplier = 1;
	public float hitEarthMultiplier = 1;
	public float hitPhysicalMultiplier = 1;
	public float splashRadiusModifier = 0;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
