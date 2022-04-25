using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class WatchVideo : MonoBehaviour {
	public static GameObject callBackObject;
	public static WatchVideo instance;
	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	public void CallRewardedAd () {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (AdmobIntilization._instance.HasRewardedAvaiable())
            {
				Time.timeScale = 0;

				AdmobIntilization._instance.ShowRewardedAds();
				//AppMetricaHandler.inst.ReportEvent("Call Admob Rewarded");

			}
			else
            {
                AdmobIntilization._instance.RequestRewardBasedVideo();

                if (Advertisement.IsReady("Rewarded_iOS"))
                {
                    var options = new ShowOptions { resultCallback = AdCallbackhanler };
                    Advertisement.Show("Rewarded_iOS", options);
					//AppMetricaHandler.inst.ReportEvent("Call Unity Rewarded");

				}
				//                else
				//                {
				//
				//                    if (IronSource.Agent.isRewardedVideoAvailable())
				//                    {
				//                        IronSource.Agent.showRewardedVideo();
				//                    }
				//                    else
				//                    {
				//                        if (AppLovin.IsIncentInterstitialReady())
				//                        {
				//                            AppLovin.ShowRewardedInterstitial();
				//
				//                        }
				//                        else
				//                        {
				//                            AppLovin.LoadRewardedInterstitial();
				ToastMessage.Instance.ShowToastMessage("Sorry,No Video Available");
//
//                        }
//                    }
//                }
            }
        }
        else {
            ToastMessage.Instance.ShowToastMessage("Sorry,No Internet Connection");
        }
    }

	void AdCallbackhanler(ShowResult result){
		switch (result){
		case ShowResult.Finished:
			Debug.Log ("Ad Finished. Rewarding player...");
			callBackObject.SendMessage ("VideoWatches");
			break;
		case ShowResult.Skipped:
			Debug.Log ("Ad Skipped");
			break;
		case ShowResult.Failed:
			Debug.Log("Ad failed");
			break;
		}
	}
}
