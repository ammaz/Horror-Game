
using UnityEngine;
using System;
using GoogleMobileAds.Api;
using GoogleMobileAds;

public class AdmobIntilization : MonoBehaviour {

	public static BannerView bannerView;
	public static InterstitialAd interstitial;
	public static RewardedAd rewardedAd;

	public static AdmobIntilization _instance;
	// Use this for initialization
	void Awake () {
		//MobileAds.SetiOSAppPauseOnBackground(true);
		//MobileAds.Initialize(initStatus => { });

		RequestRewardBasedVideo ();
		if (PlayerPrefs.GetInt ("RemoveAds") == 0) {
			RequestInterstitial ();
			RequestBanner ();
		}
		_instance = this;
	}
	


	public void RequestRewardBasedVideo()
	{

		

		if (rewardedAd == null)
		{
			rewardedAd = new RewardedAd(AdsIds.admobReward);


			// Called when an ad request has successfully loaded.
			rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
			// Called when an ad request failed to load.
			//rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
			// Called when an ad is shown.
			rewardedAd.OnAdOpening += HandleRewardedAdOpening;
			// Called when an ad request failed to show.
			rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
			// Called when the user should be rewarded for interacting with the ad.
			rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
			// Called when the ad is closed.
			rewardedAd.OnAdClosed += HandleRewardedAdClosed;

		}
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the rewarded video ad with the request.
		rewardedAd.LoadAd(request);
	}
	public void ShowRewardedAds(){
		rewardedAd.Show ();
		RequestRewardBasedVideo();

	}
	public bool HasRewardedAvaiable(){
		if (!rewardedAd.IsLoaded())
		{
			RequestRewardBasedVideo();
		}
		return rewardedAd.IsLoaded();
	}

	public bool CheckAdmob(){
		if (interstitial.IsLoaded ()) {
			return true;
		} else {
			RequestInterstitial ();
			return false;

		}

	}
	public void ShowAbMobAd(){
		interstitial.Show ();
		RequestInterstitial ();
	}

	public void RequestInterstitial()
	{

		if (interstitial != null)
		{
			if (interstitial.IsLoaded())
			{
				return;
			}
		}


		//		if (interstitial == null) {

		// Initialize an InterstitialAd.
		interstitial = new InterstitialAd (AdsIds.admobInter);

			// Called when an ad request has successfully loaded.
			//interstitial.OnAdLoaded += HandleOnAdLoadedIntersital;
			// Called when an ad request failed to load.
			//interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoadIntersital;
			// Called when an ad is shown.
			//interstitial.OnAdOpening += HandleOnAdOpenedIntersital;
			// Called when the ad is closed.
			interstitial.OnAdClosed += HandleOnAdClosedIntersital;
			// Called when the ad click caused the user to leave the application.
			//interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplicationIntersital;
//		}
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		interstitial.LoadAd(request);
	}
	public void ShowInterstialAd(){
		if (interstitial.IsLoaded())
			interstitial.Show();
		RequestInterstitial();
		
	} 
	public bool HasAdmobInterstialAvaible(){

		if (!interstitial.IsLoaded ()) {
			RequestInterstitial ();
		}
		return interstitial.IsLoaded(); 
	}
	

	public void RequestBanner()
	{


		bannerView = new BannerView(AdsIds.admobBan, AdSize.Banner, AdsIds._adsposition);

		// Called when an ad request has successfully loaded.
		bannerView.OnAdLoaded += HandleOnAdLoaded;

		// Called when an ad request failed to load.
		bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;

		// Called when an ad is clicked.
		bannerView.OnAdOpening += HandleOnAdOpened;

		// Called when the user returned from the app after an ad click.
		bannerView.OnAdClosed += HandleOnAdClosed;

		// Called when the ad click caused the user to leave the application.
		//bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();

		// Load the banner with the request.
		bannerView.LoadAd(request);
		bannerView.Show ();
	}
	public void DestoryBanner(){
		bannerView.Destroy ();
	} 
	public void HideBanner(){
		bannerView.Hide ();

	}
	public void ShowBanner(){
		bannerView.Show ();
	}

	public void AfterPurchase() {

		DestoryBanner();

	}

	#region Call Back Banner
	public void HandleOnAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLoaded event received");
	}

	public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		//MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
		//+ args.Message);
	}

	public void HandleOnAdOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdOpened event received");
	}

	public void HandleOnAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdClosed event received");
	}

	public void HandleOnAdLeavingApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLeavingApplication event received");
	}
	#endregion

	#region Admob Interstial Call Back
	public void HandleOnAdLoadedIntersital(object sender, EventArgs args)
	{
		print("HandleAdLoaded event received");
	}

	public void HandleOnAdFailedToLoadIntersital(object sender, AdFailedToLoadEventArgs args)
	{
		//print("HandleFailedToReceiveAd event received with message: "
		//+ args.Message);
	}

	public void HandleOnAdOpenedIntersital(object sender, EventArgs args)
	{
		print("HandleAdOpened event received");
	}

	public void HandleOnAdClosedIntersital(object sender, EventArgs args)
	{
		print("HandleAdClosed event received");
		Time.timeScale = 1;
		RequestInterstitial();

	}

	public void HandleOnAdLeavingApplicationIntersital(object sender, EventArgs args)
	{
		print("HandleAdLeavingApplication event received");
	}
	#endregion

	#region Admob Rewarded Video Call Back
	public void HandleRewardedAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdLoaded event received");
	}

	public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print(
			"HandleRewardedAdFailedToLoad event received with message: "
							 + args.ToString());
	}

	public void HandleRewardedAdOpening(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdOpening event received");
	}

	public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
	{
		MonoBehaviour.print(
			"HandleRewardedAdFailedToShow event received with message: "
							 + args.Message);
	}

	public void HandleRewardedAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdClosed event received");
		RequestRewardBasedVideo();
		Time.timeScale = 1;

	}
	public static void HandleUserEarnedReward(object sender, Reward args)
	{
		string type = args.Type;
		double amount = args.Amount;
		if (WatchVideo.callBackObject != null)
			WatchVideo.callBackObject.SendMessage("VideoWatches");

	}
	#endregion
}
