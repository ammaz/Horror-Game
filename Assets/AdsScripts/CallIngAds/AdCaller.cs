using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
//public enum Adspref	{none,admob,unity,lovin,iron};
public class AdCaller : MonoBehaviour {
	//public Adspref preference;
	// Use this for initialization
	/*void OnEnable () {


		//if (PlayerPrefs.GetInt("RemoveAds") == 1 || Application.internetReachability != NetworkReachability.NotReachable)
		//{
		//	return;
		//}
		if (PlayerPrefs.GetInt ("RemoveAds") == 0) {
			

		

				AdmobAd();
			


		}
	}*/
	
	public void showAd()
    {
		if (PlayerPrefs.GetInt("RemoveAds") == 0)
		{
			AdmobAd();
		}
	}

	// Update is called once per frame
	void AdmobAd () {
		//AppOpenAdManager.isInterstialAdPresent = true;
		if (AdmobIntilization._instance.CheckAdmob())
		{
			Time.timeScale = 0;
			AdmobIntilization._instance.ShowAbMobAd();

		}
		else
		{
			//if (AdmobIntilization._instance.HasRewardedAvaiable ()) {
			//	AdmobIntilization._instance.ShowRewardedAds ();
			//} else {
			//	AdmobIntilization._instance.RequestRewardBasedVideo ();

				if (Advertisement.IsReady ()) {
					Advertisement.Show ();
				}
			//}

		}
	}
	
}
