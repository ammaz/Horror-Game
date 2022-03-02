﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public AudioSource Volume;
    public Slider volumeSlider;
    public AudioSource Music;
    public Slider musicSlider;

    public FirstPersonController Sensitivity;
    public Slider sensitivitySlider;

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    public void pauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            isPaused=true;
        }
    }
}
