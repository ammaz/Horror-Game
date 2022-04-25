
using UnityEngine;
//using ChartboostSDK;

using UnityEngine.Advertisements;
using GoogleMobileAds.Api;
//using UnityEngine.Purchasing;


public class AdsIds : MonoBehaviour {
    public static AdsIds instance;
	[Header ("Admob Ids")]
	public string admobBanner;
	public string admobInterstial;
	public string admobRewarded;
	public AdPosition adsPosition;
	public AdSize adsType;

	public static string admobBan;
	public static string admobReward;
	public static string admobInter;
	public static AdPosition _adsposition;


	[Header ("Unity Ads")]

	public string unityId;



	[Header ("App Lovin")]

	public string LovinID;

	public static string _lovinid;


	[Header ("Iron Source Define")]
	public string ironSourceId;
	public static string _ironId;
    public GameObject GDPRPanel;
    //[Header("Chartboost Define")]
    //public string ChartBoostAppId;
    //public string ChartBoostSignature;
    //public static string c_appId;
    //public static string c_sig;

    [Header("Extra Stuff")]
    public string moreGames;
    public string ratesusIOS;
    public string ratesusAndroid;
    public string privacypolicy;

    public static string more_games;
    public static string rates_us;
    public static string privacy_policy;
    public bool UnlockAll=false;

    [Header ("Unity InApp Define")]

	public InAppKeys[] InAppIds;
	public static InAppKeys[] keys;

	public GameObject inAppGameObject;
	public static GameObject inappObj;

    //void OnValidate()
    //{
    //    ChartboostSDK.CBSettings.setAppId(c_appId, c_sig);
    //}


    void Awake () {
        
        if (instance != null) {

            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        admobBan = admobBanner;
		admobInter = admobInterstial;
		admobReward = admobRewarded;
		_adsposition = adsPosition;

		Advertisement.Initialize (unityId);

		//_lovinid = LovinID;


        if(UnlockAll==true)
        {
            PlayerPrefs.SetInt("RemoveAds", 1);
        }

        //Advertisement.Initialize (unityId);

        //_ironId =ironSourceId;


        more_games = moreGames;

        //if (PlayerPrefs.GetInt("privacy") == 0)
        //{
        //    GDPRPanel.SetActive(true);            
        //}


#if UNITY_IOS || UNITY_EDITOR
        rates_us = ratesusIOS;
        #endif

        #if UNITY_ANDROID || UNITY_EDITOR
		rates_us ="https://play.google.com/store/apps/details?id=" + Application.identifier;
        #endif

        privacy_policy = privacypolicy;

        keys = InAppIds;
		inappObj = inAppGameObject;
	}


    public void CloseGDPRPanel()
    {
        PlayerPrefs.SetInt("privacy", 1);
        GDPRPanel.SetActive(false);
    }
    public void RateUsLink()
    {
        
        Application.OpenURL(AdsIds.rates_us);

    }
    #region In App Call Back
    public static void AfterPurchased(int value){
		if (AdsIds.keys [value].removeads) {
			print ("remove Ads");
            PlayerPrefs.SetInt("RemoveAds", 1);
			GameObject.FindObjectOfType<AdmobIntilization>().AfterPurchase();
		}
		if (AdsIds.keys [value].unLockAll) {
			print ("UnlockALL Ads");
            PlayerPrefs.SetInt("RemoveAds", 1);
			GameObject.FindObjectOfType<AdmobIntilization>().AfterPurchase();

        }
        if (AdsIds.keys [value].rewardAmount !=0) {
			print ("Ads  Reward");
		}

	}
#endregion

}


[System.Serializable]
public class InAppKeys{
	public string Id;
	//public ProductType Type;
	public bool removeads;
	public bool unLockAll;
	public int rewardAmount;

}
