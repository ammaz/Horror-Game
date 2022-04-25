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
    //public AdCaller adCall;
    //private bool adIsRunning;

    // Start is called before the first frame update
    void Start()
    {
        //adIsRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(isLevelCompleted());
/*        if (!adIsRunning && (completedTaskCount == LevelSwitchPlayer.task.Length))
        {
            adCall.showAd();
            adIsRunning = true;
        }*/
    }

    public IEnumerator isLevelCompleted()
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
            yield return new WaitForSeconds(2f);
            LevelCompleted.SetActive(true);
            yield return new WaitForSeconds(2f);
            LevelCompleted.SetActive(false);
            loadingScreen.SetActive(true);
            StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex + 1));
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
}
