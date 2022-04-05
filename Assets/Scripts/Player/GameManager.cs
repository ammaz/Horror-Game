using System.Collections;
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
    public AudioSource Volume2;

    public AudioClip[] Sounds;

    public FirstPersonController Sensitivity;
    public Slider sensitivitySlider;

    //private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
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
        Volume2.volume = volumeSlider.value;
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

    /*public void pauseGame()
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
    }*/

    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ButtonPressed()
    {
        Volume.PlayOneShot(Sounds[1]);
    }

    public void ResumeButtonPressed()
    {
        Volume.PlayOneShot(Sounds[2]);
        Time.timeScale = 1;
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
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
}
