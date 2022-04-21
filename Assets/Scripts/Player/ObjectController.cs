using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    private FirstPersonController playerObjectController;

    // Start is called before the first frame update
    void Start()
    {
        playerObjectController = GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        //For moving Object while they are picked
        if (playerObjectController.heldObj != null)
        {
            MoveObject();
        }
    }


    //Pick/Drop Mechanics
    public void PickObject()
    {
        if (playerObjectController.heldObj == null && playerObjectController.Baby.connectedMassScale == 0)
        {
            if (Physics.Raycast(playerObjectController.playerCamera.ViewportPointToRay(playerObjectController.interactionRayPoint), out RaycastHit hit, playerObjectController.pickUpDistance) && ((hit.collider.gameObject.tag == "Toy") || (hit.collider.gameObject.tag == "Garbage") || (hit.collider.gameObject.tag == "Toys")))
            {
                PickUpObject(hit.transform.gameObject);
                //ObjectPick Sound
                Sounds.PlaySound("ObjectPick");
            }
            if (Physics.Raycast(playerObjectController.playerCamera.ViewportPointToRay(playerObjectController.interactionRayPoint), out hit, playerObjectController.interactionDistance, playerObjectController.babyLayer))
            {
                playerObjectController.BabyTP.BabyUnSit();
                
                //BabyPickUp Sound
                BabySound.PlaySound("BabyPickUp");
                //BabyPickUp Animation
                playerObjectController.BabyAnimate.BabyPickUp();
                playerObjectController.Baby.connectedMassScale = 5;
                
            }
        }
    }

    //Pick/Drop Mechanics
    void MoveObject()
    {
        if (Vector3.Distance(playerObjectController.heldObj.transform.position, playerObjectController.holdParent.position) > 0.1f)
        {
            Vector3 moveDirection = (playerObjectController.holdParent.position - playerObjectController.heldObj.transform.position);
            playerObjectController.heldObj.GetComponent<Rigidbody>().AddForce(moveDirection * playerObjectController.moveForce);
        }
    }

    //Pick/Drop Mechanics
    void PickUpObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
            objRig.useGravity = false;
            //Object will float in air(Change to get good effect)
            objRig.drag = 10;

            objRig.transform.parent = playerObjectController.holdParent;
            playerObjectController.heldObj = pickObj;
        }
    }

    //Pick/Drop Mechanics
    public void DropObject()
    {
        //Here I will disable Baby's Rigidbody and enable Animations
        if (playerObjectController.Baby.connectedMassScale == 5)
        {
            playerObjectController.Baby.connectedMassScale = 0;
            //BabyCry Sound
            BabySound.PlaySound("BabyCry");
            //BabyCry Animation
            playerObjectController.BabyAnimate.BabyCry();
        }
        else
        {
            Rigidbody heldRig = playerObjectController.heldObj.GetComponent<Rigidbody>();
            heldRig.useGravity = true;
            heldRig.drag = 1;

            playerObjectController.heldObj.transform.parent = null;
            playerObjectController.heldObj = null;

            //ObjectDrop Sound
            Sounds.PlaySound("ObjectDrop");
        }

    }
}
