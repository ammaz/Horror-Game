using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip Switch;
    public static AudioSource src;

    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        Switch = GetComponent<AudioClip>();
    }

    public static void PlaySound(string clipName)
    {
        switch (clipName)
        {
            case "Switch":
                //src.PlayOneShot(Switch);
                break;
        }
    }
}
