using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioSource Volume;
    public Slider volumeSlider;
    public AudioClip[] Sounds;

    public AudioSource Music;
    public Slider musicSlider;

    public FirstPersonController Sensitivity;
    public Slider sensitivitySlider;

    private int levelIndex;

    public Text levelInfo;

    //For Loading Slider
    public Slider slider;
    public Text progressText;

    public GameObject loadingScreen;
    public GameObject levelScreen;

    // Start is called before the first frame update
    void Start()
    {
        levelIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void muteAudio()
    { 
        Music.volume = 0;
    }

    public void unmuteMusic()
    {
        //Subject to change
        Music.volume = 1;
    }

    public void muteVolume()
    {
        Music.volume = 0;
        Volume.volume = 0;
    }

    public void unmuteVolume()
    {
        Music.volume = 1;
        Volume.volume = 1;
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void SetVolume()
    {
        Volume.volume = volumeSlider.value;
        Music.volume = volumeSlider.value;
    }

    public void SetMusic()
    {
        Music.volume = musicSlider.value;
    }

    public void SetSensitivity()
    {
        Sensitivity.mouseSensitivity = sensitivitySlider.value;
    }

    public void ButtonPressed() 
    {
        Volume.PlayOneShot(Sounds[1]);
    }

    public void PlayButtonPressed()
    {
        Volume.PlayOneShot(Sounds[2]);
    }

    public void playGame()
    {
        if (levelIndex == 0)
        {
            levelInfo.text = "Please select a level";
        }

        else if (levelIndex==2 || levelIndex == 3 || levelIndex == 4 || levelIndex == 5)
        {
            levelInfo.text = "\n\nLevel Coming Soon";
        }

        else
        {
            levelScreen.SetActive(false);
            loadingScreen.SetActive(true);
            StartCoroutine(LoadAsynchronously(levelIndex));      
        }
    }

    public void LoadLevel(int sceneIndex)
    {
        levelIndex = sceneIndex;
        if (levelIndex == 1)
        {
            levelInfo.text = "Night 1\n\nThank God it was just a dream";
        }
        else if (levelIndex == 2)
        {
            levelInfo.text = "Night 2\n\nThe house is still so messy, let’s clean it up";
        }
        else if (levelIndex == 3)
        {
            levelInfo.text = "Night 3\n\nHe is just a weird baby";
        }
        else if (levelIndex == 4)
        {
            levelInfo.text = "Night 4\n\nThe baby wants to play";
        }
        else if (levelIndex == 5)
        {
            levelInfo.text = "Night 5\n\nBaby needs your help";
        }
        else
        {
            levelInfo.text = "";
        }
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
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
