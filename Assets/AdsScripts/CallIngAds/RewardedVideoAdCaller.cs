using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RewardedVideoAdCaller : MonoBehaviour {
	public static bool isRewardAd = false;

	// Use this for initialization
	void OnEnable () {
		if (PlayerPrefs.GetInt ("RemoveAds") == 0) {
			if (PlayerPrefs.GetInt (gameObject.name) != 1) {
				GetComponent<Button> ().enabled = false;
			} else {
				VideoWatches ();
			}
		} 
		else {
			VideoWatches ();
		}
	}

	public void WatchRewardedVideo(){
		if (Application.internetReachability != NetworkReachability.NotReachable) {
			WatchVideo.callBackObject = gameObject;
			WatchVideo.instance.CallRewardedAd ();
		} else {
            ToastMessage.Instance.ShowToastMessage("No, Network Available");
		}

	}

	void VideoWatches () {
		ToastMessage.Instance.ShowToastMessage ("Rewarded Ad Showed Give Reward");
		print("Rewarded Ad Showed Give Reward");
		PlayerPrefs.GetInt (gameObject.name, 1);

		Destroy(gameObject);
	}


}
