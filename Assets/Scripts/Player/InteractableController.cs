using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
    private FirstPersonController playerInteractableController;

    // Start is called before the first frame update
    void Start()
    {
        playerInteractableController = GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInteractableController.canInteract)
        {
            HandleInteractionCheck();
            //HandleInteractionInput();
            InteractionCheckText();
        }
    }

    /// <summary>
    /// Interaction
    /// </summary>

    //I have kept this for doors only
    //Constantly raycast and look for interactable objects
    private void HandleInteractionCheck()
    {
        if (Physics.Raycast(playerInteractableController.playerCamera.ViewportPointToRay(playerInteractableController.interactionRayPoint), out RaycastHit hit, playerInteractableController.interactionDistance))
        {
            //Change number 8 to Interactable Layer (Subject to change)
            if (hit.collider.gameObject.layer == 8 && (playerInteractableController.currentInteractable == null || hit.collider.gameObject.GetInstanceID() != playerInteractableController.currentInteractable.GetInstanceID()))
            {
                hit.collider.TryGetComponent(out playerInteractableController.currentInteractable);

                if (playerInteractableController.currentInteractable)
                {
                    playerInteractableController.currentInteractable.OnFocus();
                    //Changing name of InteractionText
                    //InteractText.SetInteractText("" + currentInteractable.name);
                    //SimpleInteractText.text = "" + currentInteractable.name;
                }
            }
        }
        else if (playerInteractableController.currentInteractable)
        {
            playerInteractableController.currentInteractable.OnLoseFocus();
            playerInteractableController.currentInteractable = null;
            //Removing name of InteractionText
            //InteractText.RemoveInteractText();
            //SimpleInteractText.text = "";
        }
    }

    //For changing the text
    private void InteractionCheckText()
    {
        if (Physics.Raycast(playerInteractableController.playerCamera.ViewportPointToRay(playerInteractableController.interactionRayPoint), out RaycastHit hit, playerInteractableController.interactionDistance, playerInteractableController.interactionLayer))
        {
            playerInteractableController.SimpleInteractText.text = "" + hit.transform.gameObject.name;
            if (playerInteractableController.heldObj != null)
            {
                if (playerInteractableController.heldObj.name == "Plate" && playerInteractableController.SimpleInteractText.text == "Sink")
                {
                    playerInteractableController.SimpleInteractText.text = "Wash Plate";
                }

                if (playerInteractableController.heldObj.name == "Magic Wand" && playerInteractableController.SimpleInteractText.text == "Soul Basket")
                {
                    playerInteractableController.SimpleInteractText.text = "Turn into holy soul";
                }
            }
        }
        else if (Physics.Raycast(playerInteractableController.playerCamera.ViewportPointToRay(playerInteractableController.interactionRayPoint), out hit, playerInteractableController.interactionDistance, playerInteractableController.babyLayer))
        {
            if (playerInteractableController.heldObj != null)
            {
                if (playerInteractableController.heldObj.name == "Feeder")
                {
                    playerInteractableController.SimpleInteractText.text = "Feed the baby";
                }
                else if (playerInteractableController.heldObj.name == "Diaper")
                {
                    playerInteractableController.SimpleInteractText.text = "Change baby diaper";
                }
                else if (playerInteractableController.heldObj.name == "Magic Wand")
                {
                    playerInteractableController.SimpleInteractText.text = "Use magic wand";
                }
            }
            else
            {
                playerInteractableController.SimpleInteractText.text = "Baby";
            }
        }
        else
        {
            playerInteractableController.SimpleInteractText.text = "";
        }
    }

    //When we hit interact key the action will be performed
    //For Door Opening
    public void HandleInteractionInput()
    {
        //Interaction key, I will add button and touch controls in this (Subject to change)
        if (playerInteractableController.currentInteractable != null && Physics.Raycast(playerInteractableController.playerCamera.ViewportPointToRay(playerInteractableController.interactionRayPoint), out RaycastHit hit, playerInteractableController.interactionDistance, playerInteractableController.interactionLayer))
        {
            playerInteractableController.currentInteractable.OnInteract();
        }

        if (playerInteractableController.heldObj != null)
        {
            if (((playerInteractableController.heldObj.name == "Feeder") || (playerInteractableController.heldObj.name == "Diaper")) && ((playerInteractableController.SimpleInteractText.text == "Feed the baby") || (playerInteractableController.SimpleInteractText.text == "Change baby diaper")))
            {
                playerInteractableController.GiveButtonPressed = true;

                if (playerInteractableController.heldObj.name == "Diaper")
                {
                    //ChangeBabyDiaper Sound
                    BabySound.PlaySound("ChangeBabyDiaper");

                    //Baby Happy Animation
                    playerInteractableController.BabyAnimate.BabyHappy();
                }
                else
                {
                    //BabyFeeder Sound
                    BabySound.PlaySound("BabyFeeder");

                    //Baby Happy Animation
                    playerInteractableController.BabyAnimate.BabyHappy();
                }

                //Destroying Feeder or Diaper
                Destroy(playerInteractableController.heldObj, 0.3f);
            }
            else if (playerInteractableController.heldObj.name == "Plate" && playerInteractableController.SimpleInteractText.text == "Wash Plate")
            {
                playerInteractableController.GiveButtonPressed = true;
                //Destroying Plate
                Destroy(playerInteractableController.heldObj, 0.3f);
            }
            else if (playerInteractableController.heldObj.name == "Shirt" && playerInteractableController.SimpleInteractText.text == "Washing Machine")
            {
                playerInteractableController.GiveButtonPressed = true;
                //Destroying Shirt
                Destroy(playerInteractableController.heldObj, 0.3f);
            }
            else if (playerInteractableController.heldObj.name == "Key" && playerInteractableController.SimpleInteractText.text == "Locked")
            {
                playerInteractableController.GiveButtonPressed = true;
                //Destroying Key
                Destroy(playerInteractableController.heldObj, 0.3f);
            }
            else if (playerInteractableController.heldObj.name == "Magic Wand" && playerInteractableController.SimpleInteractText.text == "Use magic wand")
            {
                playerInteractableController.GiveButtonPressed = true;
                //Destroying Magic Wand
                Destroy(playerInteractableController.heldObj, 0.3f);
                //Baby Happy Animation
                playerInteractableController.BabyAnimate.BabyHappy();
            }
            else if (playerInteractableController.heldObj.name == "Magic Wand" && playerInteractableController.SimpleInteractText.text == "Turn into holy soul")
            {
                playerInteractableController.GiveButtonPressed = true;
            }
            else
            {
                playerInteractableController.GiveButtonPressed = false;
            }
        }

        //Washing face
        if (playerInteractableController.SimpleInteractText.text == "Wash face")
        {
            playerInteractableController.GiveButtonPressed = true;
        }

        //Turning on/off TV
        if (playerInteractableController.SimpleInteractText.text == "TV")
        {
            if (playerInteractableController.SimpleTV.activeSelf == true)
                playerInteractableController.SimpleTV.SetActive(false);
            else
                playerInteractableController.SimpleTV.SetActive(true);
        }

        //Baby Sit
        if (playerInteractableController.Baby.connectedMassScale == 5 && playerInteractableController.SimpleInteractText.text == "Baby Chair")
        {
            //BabyTPSit
            playerInteractableController.BabyTP.BabyTPSit();
        }

        //Turning on/off Switch
        if (playerInteractableController.SimpleInteractText.text == "Switch")
        {
            //On
            if (playerInteractableController.Switch.GetBool("Off"))
            {
                playerInteractableController.Switch.SetBool("On", true);
                playerInteractableController.Switch.SetBool("Off", false);
                playerInteractableController.BabyRoomLight.SetActive(true);
                Sounds.PlaySound("Switch");
            }
            //Off
            else
            {
                playerInteractableController.Switch.SetBool("Off", true);
                playerInteractableController.Switch.SetBool("On", false);
                playerInteractableController.BabyRoomLight.SetActive(false);
                Sounds.PlaySound("Switch");
            }
        }

        //Sitting on couch
        if (Physics.Raycast(playerInteractableController.playerCamera.ViewportPointToRay(playerInteractableController.interactionRayPoint), out hit, playerInteractableController.interactionDistance, playerInteractableController.interactionLayer))
        {
            if (playerInteractableController.SimpleInteractText.text == "Couch")
            {
                if (playerInteractableController.canMove == true)
                {
                    StartCoroutine(playerInteractableController.SitonCouch(hit.transform.gameObject));
                    playerInteractableController.NoSignalTV.SetActive(true);
                }
                else if (playerInteractableController.canMove == false)
                {
                    playerInteractableController.canMove = true;
                    playerInteractableController.crouch.gameObject.SetActive(true);
                    playerInteractableController.uncrouch.gameObject.SetActive(false);
                    playerInteractableController.jump.gameObject.SetActive(true);
                    StartCoroutine(playerInteractableController.CrouchStand());
                    playerInteractableController.NoSignalTV.SetActive(false);
                }
            }
        }
    }
}
