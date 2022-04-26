using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSwitch : MonoBehaviour
{
    public FirstPersonController LevelSwitchPlayer;
    private int completedTaskCount;

    public GameObject loadingScreen;
    public GameObject LevelCompleted;

    //For Loading Slider
    public Slider slider;
    public Text progressText;

    //For Ads
    public AdCaller adCall;

    //Level loading
    private bool isloading;

    // Start is called before the first frame update
    void Start()
    {
        isloading = false;
        //Hiding Banner
        AdmobIntilization._instance.HideBanner();
    }

    // Update is called once per frame
    void Update()
    {
        if (areAllTaskCompleted() && !isloading)
        {
            isloading = true;
            StartCoroutine(isLevelCompleted());
        } 
    }

    public IEnumerator isLevelCompleted()
    {
        yield return new WaitForSeconds(4f);
        LevelCompleted.SetActive(true);
        yield return new WaitForSeconds(2f);
        adCall.showAd();
        yield return new WaitForSeconds(2f);
        LevelCompleted.SetActive(false);
        loadingScreen.SetActive(true);
        if (!(SceneManager.GetActiveScene().name=="Level 5"))
        {
            StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex + 1));
        }
        else
        {
            //If we are in level 5 go back to main menu once game is completed
            StartCoroutine(LoadAsynchronously(0));
        }
    }

    public IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            //Curent progress
            slider.value = progress;
            progressText.text = (int)progress * 100 + "%";

            yield return null;
        }
    }

    public bool areAllTaskCompleted()
    {
        completedTaskCount = 0;

        for (int a = 0; a < LevelSwitchPlayer.task.Length; a++)
        {
            if (LevelSwitchPlayer.task[a].Completed)
            {
                completedTaskCount++;
            }
        }

        if (completedTaskCount == LevelSwitchPlayer.task.Length)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
