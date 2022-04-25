using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MainMenuButtons : MonoBehaviour {
    [SerializeField]
    private GameObject ExistPanel;

    [SerializeField]
    private GameObject RateUs;

    [SerializeField]
    private GameObject GDPRPanel;

	[SerializeField]
	private GameObject InAppPanel;

    bool ispanel;

	public Sprite soundon;
	public Sprite soundoff;

    void Start()
    {
    
        if (PlayerPrefs.GetInt("privacy") == 0)
        {
            GDPRPanel.SetActive(true);
            ispanel = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {

            if (!ispanel)
            {
                ispanel = true;
                if (PlayerPrefs.GetInt("rateus") == 0)
                {
                    RateUs.SetActive(true);


                }
                else
                {
                    ExistPanel.SetActive(true);


                }
            }
            else
            {
                ClosePanel();


            }
        }
    }

	void SoundHandle(int no){
		
		if(no==1){
			GameObject.Find ("sound-ON").GetComponent<SpriteRenderer> ().sprite = soundoff;
//			GameObject.Find ("sound-ON").GetComponent<tk2dButton> ().ObjectNumber = 2;
			AudioListener.volume = 0;				
		}
		if(no==2){
			GameObject.Find ("sound-ON").GetComponent<SpriteRenderer> ().sprite = soundon;
//			GameObject.Find ("sound-ON").GetComponent<tk2dButton> ().ObjectNumber = 1;
			AudioListener.volume = 1;

		}
	}

    public void ClosePanel()
    {
        RateUs.SetActive(false);
        ExistPanel.SetActive(false);
		InAppPanel.SetActive (false);
        ispanel = false;
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
	public void BuyInAppPanelOpen(){
		

		InAppPanel.SetActive (true);
	}
	public void RateUsPanelOpen(){
		

		RateUs.SetActive (true);
	}
    public void CloseGDPRPanel()
    {
        PlayerPrefs.SetInt("privacy", 1);
        GDPRPanel.SetActive(false);
    }

    public void MoreGames() {
        Application.OpenURL(AdsIds.more_games);


    }
    public void RateUsLink() {
		PlayerPrefs.SetInt("rateus", 1);
        Application.OpenURL(AdsIds.rates_us);

    }
    public void PrivacyPolicyLink()
    {
        Application.OpenURL(AdsIds.privacy_policy);

    }
}
