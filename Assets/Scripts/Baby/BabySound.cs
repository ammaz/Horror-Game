using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabySound : MonoBehaviour
{
    //Baby Sounds
    public static AudioClip BabyCry;
    public static AudioClip BabyHappy;
    public static AudioClip BabyFeeder;
    public static AudioClip BabyAngry;
    public static AudioClip BabyPickUp;
    public static AudioClip ChangeBabyDiaper;

    //AudioSource Baby
    public static AudioSource BabyAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        //Baby Sounds
        BabyCry = Resources.Load<AudioClip>("Sounds/BabyCry");
        BabyHappy = Resources.Load<AudioClip>("Sounds/BabyHappy");
        BabyFeeder = Resources.Load<AudioClip>("Sounds/BabyFeeder");
        /*BabyAngry;*/
        BabyPickUp = Resources.Load<AudioClip>("Sounds/BabyPickUp");
        ChangeBabyDiaper = Resources.Load<AudioClip>("Sounds/ChangeBabyDiaper");

        //AudioSource
        BabyAudioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clipName)
    {
        switch (clipName)
        {
            case "BabyCry":
                BabyAudioSource.Stop();
                BabyAudioSource.PlayOneShot(BabyCry);
                break;
        }

        switch (clipName)
        {
            case "BabyHappy":
                BabyAudioSource.Stop();
                BabyAudioSource.PlayOneShot(BabyHappy);
                break;
        }

        switch (clipName)
        {
            case "BabyFeeder":
                BabyAudioSource.Stop();
                BabyAudioSource.PlayOneShot(BabyFeeder);
                break;
        }

        switch (clipName)
        {
            case "BabyPickUp":
                BabyAudioSource.Stop();
                BabyAudioSource.PlayOneShot(BabyPickUp);
                break;
        }

        switch (clipName)
        {
            case "ChangeBabyDiaper":
                BabyAudioSource.Stop();
                BabyAudioSource.PlayOneShot(ChangeBabyDiaper);
                break;
        }
    }
}
