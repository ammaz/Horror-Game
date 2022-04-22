using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    //Switch on/off Sound
    public static AudioClip Switch;
    /*
    //Task Sound
    public static AudioClip TaskCompleted;
    public static AudioClip TaskFailed;
    */
    //Object Pick/Drop Sound
    public static AudioClip ObjectPick;
    public static AudioClip ObjectDrop;

    //Object Spawn Sound
    public static AudioClip ObjectSpawn;

    //Object Fly Sound
    public static AudioClip ObjectFly;
    /*
    //Object Drop Sounds
    public static AudioClip FeederDrop;
    public static AudioClip PlateDrop;
    public static AudioClip ToyDrop;
    public static AudioClip DiaperDrop;
    public static AudioClip GarbageDrop;
    */
    /*
    //Wash Sounds
    public static AudioClip PlateWash;
    public static AudioClip WashingMachine;
    //public static AudioClip ChangeBabyDiaper;
    */
    /*
    //Player Sounds
    public static AudioClip Crouch;
    public static AudioClip Jump;
    public static AudioClip SitOnCouch;
    public static AudioClip GettingUpFromCouch;
    */

    //Door Sound
    public static AudioClip HorrorDoorKnock;
    //AudioSoruce
    public static AudioSource src;

    // Start is called before the first frame update
    void Start()
    {
        //Switch on/off Sound
        Switch = Resources.Load<AudioClip>("Sounds/Switch");

        /*
        //Task Sound
        TaskCompleted;
        TaskFailed;
        */

        //Object Pick Sound
        ObjectPick = Resources.Load<AudioClip>("Sounds/ObjectPick");

        //Object Drop Sounds
        ObjectDrop = Resources.Load<AudioClip>("Sounds/ObjectDrop");
        /*
        FeederDrop;
        PlateDrop;
        ToyDrop;
        DiaperDrop;
        GarbageDrop;
        */

        //Object Spawn Sound
        ObjectSpawn = Resources.Load<AudioClip>("Sounds/ObjectSpawn");

        //Object Fly Sound
        ObjectFly = Resources.Load<AudioClip>("Sounds/Objectfly");

        //Wash Sounds
        //ChangeBabyDiaper = Resources.Load<AudioClip>("Sounds/ChangeBabyDiaper");
        /*PlateWash;
        WashingMachine;*/

        /*
        //Player Sounds
        Crouch;
        Jump;
        SitOnCouch;
        GettingUpFromCouch;
        */

        //Door Sound
        HorrorDoorKnock = Resources.Load<AudioClip>("Sounds/HorrorDoorKnock");
        //AudioSource
        src = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clipName)
    {
        switch (clipName)
        {
            case "Switch":
                src.PlayOneShot(Switch);
                break;
        }

        switch (clipName)
        {
            case "ObjectPick":
                src.PlayOneShot(ObjectPick);
                break;
        }

        switch (clipName)
        {
            case "ObjectDrop":
                src.PlayOneShot(ObjectDrop);
                break;
        }

        switch (clipName)
        {
            case "ObjectSpawn":
                src.Stop();
                src.PlayOneShot(ObjectSpawn);
                break;
        }
        switch (clipName)
        {
            case "HorrorDoorKnock":
                src.Stop();
                src.PlayOneShot(HorrorDoorKnock);
                break;
        }
        switch (clipName)
        {
            case "ObjectFly":
                src.Stop();
                src.PlayOneShot(ObjectFly);
                break;
        }
    }
}
