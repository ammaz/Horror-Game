using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioClip doorOpen;
    public AudioClip doorClose;
    public AudioClip horrorDoorClose;

    public AudioSource Audio;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string clipName)
    {
        switch (clipName)
        {
            case "DoorOpen":
                Audio.PlayOneShot(doorOpen);
                break;
        }
        switch (clipName)
        {
            case "DoorClose":
                Audio.PlayOneShot(doorClose);
                break;
        }
        switch (clipName)
        {
            case "HorrorDoorClose":
                Audio.PlayOneShot(horrorDoorClose);
                break;
        }
    }
}
