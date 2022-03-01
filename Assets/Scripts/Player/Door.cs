using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    private bool isOpen = false;
    private bool canBeInteractactedWith = true;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
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

            StartCoroutine(AutoClose());
        }
    }

    public override void OnLoseFocus()
    {
        
    }

    private IEnumerator AutoClose()
    {
        yield return new WaitForSeconds(5);

        if (Vector3.Distance(transform.position, FirstPersonController.instance.transform.position) > 3)
        {
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
}
