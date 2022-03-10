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

    // Start is called before the first frame update
    void Start()
    {

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

    public void playGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(1);
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
}
