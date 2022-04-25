using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateUsSelection : MonoBehaviour {
	public static int counter = 1;
	public GameObject RateusObj;
	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetInt ("rateus") == 0){
			counter++;
		if (counter % 4 == 0) {
			counter = 2;
			RateusObj.SetActive (true);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Rate()
    {
		PlayerPrefs.SetInt("rateus", 1);
		Application.OpenURL(AdsIds.rates_us);
    }

	void Later(){
		RateusObj.SetActive (false);
	}
}
