using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public bool isOpen = false;
    private bool canBeInteractactedWith = true;
    private Animator anim;

    //For AudioSource
    private AudioSource Audio;

    //For AudioClips
    private AudioClip doorOpen;
    private AudioClip doorClose;
    private AudioClip horrorDoorClose;
    private AudioClip horrorDoorKnock;

    private void Start()
    {
        anim = GetComponent<Animator>();
        Audio = GetComponent<AudioSource>();

        //Passing AudioClips
        doorOpen = Resources.Load<AudioClip>("Sounds/DoorOpen");
        doorClose = Resources.Load<AudioClip>("Sounds/DoorClose");
        horrorDoorClose = Resources.Load<AudioClip>("Sounds/DoorCloseHorror");
        horrorDoorKnock = Resources.Load<AudioClip>("HorrorDoorKnock");
    }

    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        if (canBeInteractactedWith)
        {
            isOpen = !isOpen;

            Vector3 doorTransformDirection = transform.TransformDirection(Vector3.forward);
            Vector3 playerTransformDirection = FirstPersonController.instance.transform.position - transform.position;
            //Will have to change dot value to -90 if things doesnt work
            float dot = Vector3.Dot(doorTransformDirection, playerTransformDirection);
            anim.SetFloat("dot",dot);
            anim.SetBool("isOpen",isOpen);
            if (isOpen)
            {
                PlaySound("DoorOpen");
            }
            else if(!isOpen)
            {
                PlaySound("DoorClose");
            }

            StartCoroutine(AutoClose());
        }

        if(canBeInteractactedWith && gameObject.tag == "FridgeDoor")
        {
            //isOpen = !isOpen;

            Vector3 doorTransformDirection = transform.TransformDirection(Vector3.forward);
            Vector3 playerTransformDirection = FirstPersonController.instance.transform.position - transform.position;
            //Will have to change dot value to -90 if things doesnt work
            float dot = Vector3.Dot(doorTransformDirection, playerTransformDirection);

            anim.SetFloat("dot", dot);
            anim.SetBool("isOpen", isOpen);

            if (isOpen)
            {
                PlaySound("DoorOpen");
            }
            else if (!isOpen)
            {
                PlaySound("DoorClose");
            }
        }
    }

    public override void OnLoseFocus()
    {
        
    }

    public IEnumerator AutoClose()
    {
        yield return new WaitForSeconds(5);

        if (Vector3.Distance(transform.position, FirstPersonController.instance.transform.position) > 3)
        {
            if (isOpen)
            {
                PlaySound("HorrorDoorClose");
            }
            isOpen = false;
            anim.SetFloat("dot", 0);
            anim.SetBool("isOpen", isOpen);
        }
    }

    private void Animator_LockInteraction()
    {
        canBeInteractactedWith = false;
    }

    private void Animator_UnlockInteraction()
    {
        canBeInteractactedWith = true;
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
        switch (clipName)
        {
            case "HorrorDoorKnock":
                Audio.PlayOneShot(horrorDoorKnock);
                break;
        }
    }
}
