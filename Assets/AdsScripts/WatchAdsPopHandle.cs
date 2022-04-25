using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WatchAdsPopHandle : MonoBehaviour {



	void OnEnable () {

		if (PlayerPrefs.GetInt ("RemoveAds") == 0) {
			if (PlayerPrefs.GetInt (gameObject.name) == 0) {
				print ("BtnDisable");
//				if (transform.parent.GetComponent<tk2dButton> ()) {
//					transform.parent.GetComponent<tk2dButton> ().enabled = false;
//				}
					
				if (transform.parent.GetComponent<BoxCollider> ()) {
					transform.parent.GetComponent<BoxCollider> ().enabled = false;

				}
				if (transform.parent.GetComponent<Button> ()) {
					transform.parent.GetComponent<Button> ().enabled = false;

				}
			} 
			else {
				OnCompleteVideoWatches ();
			}
		} 
		else {
			OnCompleteVideoWatches ();
		}
	}
	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnCompleteVideoWatches () {
		
		PlayerPrefs.SetInt (gameObject.name, 1);
//		if (transform.parent.GetComponent<tk2dButton> ()) {
//			transform.parent.GetComponent<tk2dButton> ().enabled = true;
//		}
		if (transform.parent.GetComponent<BoxCollider> ()) {
			transform.parent.GetComponent<BoxCollider> ().enabled = true;
		}
		if (transform.parent.GetComponent<Button> ()) {
			transform.parent.GetComponent<Button> ().enabled = true;
		}
		Destroy(gameObject);

//		WatchAdsPopUpControll.instance.WatchAdsPopClose ();
	}
	public void ScoreCheckingComplete () {

		PlayerPrefs.SetInt (gameObject.name, 1);
//		if (transform.parent.GetComponent<tk2dButton> ()) {
//			transform.parent.GetComponent<tk2dButton> ().enabled = true;
//		}
		if (transform.parent.GetComponent<BoxCollider> ()) {
			transform.parent.GetComponent<BoxCollider> ().enabled = true;
		}
		if (transform.parent.GetComponent<Button> ()) {
			transform.parent.GetComponent<Button> ().enabled = true;
		}
		Destroy(gameObject);

//		SelectionScene.instance.ScorePopClose ();
	}
}
