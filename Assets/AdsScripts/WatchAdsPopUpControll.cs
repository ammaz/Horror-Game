using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WatchAdsPopUpControll : MonoBehaviour {
	public static WatchAdsPopUpControll instance;

	public GameObject WatchAdsPop;
	public GameObject UiLayer;
	public static GameObject _refAdsPopObj;
	public GameObject []AdsObj;

	// Use this for initialization
	void Start () {
		instance = this;
	}


	public void WatchAdsPopOpen(int ObjNo){
		_refAdsPopObj = AdsObj[ObjNo];
		WatchAdsPop.SetActive (true);
		if(UiLayer!=null)			
		UiLayer.SetActive (true);
	}


	public void WatchAdsPopClose(){
		WatchAdsPop.SetActive (false);
		if(UiLayer!=null)
		UiLayer.SetActive (false);

	}










	void OnClickNext(){
		SceneManager.LoadScene ("");
	}

	// Update is called once per frame
	void Update () {

	}
}
