using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleButtons : MonoBehaviour
{
    private FirstPersonController HandleButtonsPlayer;

    // Start is called before the first frame update
    void Start()
    {
        HandleButtonsPlayer = GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HandleButtonsPlayer.canMove)
        {
            //For Buttons
            PlayerHandleButtons();
        }
    }

    public void PlayerHandleButtons()
    {
        //For Interact Button
        if (HandleButtonsPlayer.currentInteractable != null && Physics.Raycast(HandleButtonsPlayer.playerCamera.ViewportPointToRay(HandleButtonsPlayer.interactionRayPoint), out RaycastHit hit, HandleButtonsPlayer.interactionDistance, HandleButtonsPlayer.interactionLayer))
        {
            HandleButtonsPlayer.interact.gameObject.SetActive(true);
        }
        else if (HandleButtonsPlayer.heldObj != null)
        {
            if (((HandleButtonsPlayer.heldObj.name == "Feeder") || (HandleButtonsPlayer.heldObj.name == "Diaper")) && ((HandleButtonsPlayer.SimpleInteractText.text == "Feed the baby") || (HandleButtonsPlayer.SimpleInteractText.text == "Change baby diaper")))
            {
                HandleButtonsPlayer.interact.gameObject.SetActive(true);
            }
            else if (HandleButtonsPlayer.heldObj.name == "Plate" && HandleButtonsPlayer.SimpleInteractText.text == "Wash Plate")
            {
                HandleButtonsPlayer.interact.gameObject.SetActive(true);
            }
            else if (HandleButtonsPlayer.heldObj.name == "Shirt" && HandleButtonsPlayer.SimpleInteractText.text == "Washing Machine")
            {
                HandleButtonsPlayer.interact.gameObject.SetActive(true);
            }
            else if (HandleButtonsPlayer.heldObj.name == "Key" && HandleButtonsPlayer.SimpleInteractText.text == "Locked")
            {
                HandleButtonsPlayer.interact.gameObject.SetActive(true);
            }
            else if (HandleButtonsPlayer.heldObj.name == "Magic Wand" && (HandleButtonsPlayer.SimpleInteractText.text == "Use magic wand" || HandleButtonsPlayer.SimpleInteractText.text == "Turn into holy soul"))
            {
                HandleButtonsPlayer.interact.gameObject.SetActive(true);
            }
            else
            {
                HandleButtonsPlayer.interact.gameObject.SetActive(false);
            }
        }
        else if (HandleButtonsPlayer.SimpleInteractText.text == "Couch" || HandleButtonsPlayer.SimpleInteractText.text == "TV" || HandleButtonsPlayer.SimpleInteractText.text == "Switch" || HandleButtonsPlayer.SimpleInteractText.text == "Wash face")
        {
            HandleButtonsPlayer.interact.gameObject.SetActive(true);
        }
        //Baby Sit
        else if (HandleButtonsPlayer.Baby.connectedMassScale == 5 && HandleButtonsPlayer.SimpleInteractText.text == "Baby Chair" && HandleButtonsPlayer.heldObj == null)
        {
            HandleButtonsPlayer.interact.gameObject.SetActive(true);
        }
        else
        {
            HandleButtonsPlayer.interact.gameObject.SetActive(false);
        }


        //For Pick Button
        if (HandleButtonsPlayer.heldObj == null && HandleButtonsPlayer.Baby.connectedMassScale == 0)
        {
            if (Physics.Raycast(HandleButtonsPlayer.playerCamera.ViewportPointToRay(HandleButtonsPlayer.interactionRayPoint), out hit, HandleButtonsPlayer.pickUpDistance) && ((hit.collider.gameObject.tag == "Toy") || (hit.collider.gameObject.tag == "Garbage") || (hit.collider.gameObject.tag == "Toys")) || (Physics.Raycast(HandleButtonsPlayer.playerCamera.ViewportPointToRay(HandleButtonsPlayer.interactionRayPoint), out hit, HandleButtonsPlayer.interactionDistance, HandleButtonsPlayer.babyLayer)))
            {
                HandleButtonsPlayer.pick.gameObject.SetActive(true);
            }
            else
            {
                HandleButtonsPlayer.pick.gameObject.SetActive(false);
            }
        }

        //For Drop Button
        if (HandleButtonsPlayer.heldObj != null || HandleButtonsPlayer.Baby.connectedMassScale == 5)
        {
            HandleButtonsPlayer.drop.gameObject.SetActive(true);
            HandleButtonsPlayer.pick.gameObject.SetActive(false);
        }
        else
        {
            HandleButtonsPlayer.drop.gameObject.SetActive(false);
        }
    }
}
