using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConsumeAbleAds : MonoBehaviour {


    public static bool isRewardAd = false;

    // Use this for initialization
    void Start()
    {
     
    }
    public void WatchRewardedVideo()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            WatchVideo.callBackObject = gameObject;
            WatchVideo.instance.CallRewardedAd();
        }
        else
        {
            ToastMessage.Instance.ShowToastMessage("No, Network Available");
        }

    }

    void VideoWatches()
    {
        ToastMessage.Instance.ShowToastMessage("Rewarded Ad Showed Give Reward");
        print("Rewarded Ad Showed Give Reward");
       /* GameManager.instance.RewardUnlockSkin();

        if (SceneManager.GetActiveScene().name == "Menu")
        {
            GameManager.instance.RewardUnlockSkin();

            //this.GameState.gameObject.GetComponent<GameManager>().RewardUnlockSkin();
        }
        else
        {
            barre.RewardDisplay = true;
        }
        //      PlayerPrefs.GetInt (gameObject.name, 1);

        //      Destroy(gameObject);*/
    }


}

