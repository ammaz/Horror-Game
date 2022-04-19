using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.Dynamics;

public class Baby : MonoBehaviour
{
    public Transform Player;
    public FirstPersonController playerCharacter;

    public static int BabyNumHideSeek;
    public HeadController BabyHeadController;
    public PuppetMaster puppetMaster;

    public bool isCoroutineReady;

    public Transform BabyParent;

    public bool BabyisLookingPlayer;

    void Start()
    {
        BabyNumHideSeek = 0;
        isCoroutineReady = false;
        BabyisLookingPlayer = false;
    }

    public void BabyTPSit()
    {
        //Baby Sit Animation
        playerCharacter.BabyAnimate.BabySit();

        //BabyHappy Sound
        BabySound.PlaySound("BabyHappy");

        playerCharacter.Baby.connectedMassScale = 0;
        puppetMaster.pinWeight = 1;
        BabyParent.position = new Vector3(-6.8373f, 0.8f, -7.096f);
        puppetMaster.GetComponent<PuppetMaster>().enabled = false;
        puppetMaster.GetComponent<PuppetMaster>().enabled = true;
        BabyParent.Rotate(new Vector3(0, 180, 0));
    }

    public void BabyUnSit()
    { 
        if(BabyParent.rotation.y != -90 && puppetMaster.pinWeight != 0)
        {
            puppetMaster.pinWeight = 0;
            BabyParent.Rotate(new Vector3(0, 180, 0));
        }
    }

    public void BabyTP(float x,float y,float z)
    {
        gameObject.transform.position = new Vector3(x, y, z);
    }

    public void BabyHideAndSeek()
    {
        if (Vector3.Distance(Player.position, this.transform.position) < 4f)
        {
            BabyTP(Random.Range(-8,8),2f,Random.Range(-6,9));
            BabyNumHideSeek++;
            //BabyFindMe Sound
            BabySound.PlaySound("BabyHappy");
        }
    }

    public IEnumerator BabyLookAtPlayer()
    {
        isCoroutineReady = true;

        BabyisLookingPlayer = true;

        //BabySinging Sound
        //BabySound.PlaySound("BabySinging");

        //Enabling Headlook
        BabyHeadController.GetComponent<HeadController>().enabled = true;

        yield return new WaitForSeconds(Random.Range(5, 10));

        BabyisLookingPlayer = false;
        //Disabling Headlook
        BabyHeadController.GetComponent<HeadController>().enabled = false;
        //BabyHorror Sound
        //BabySound.PlaySound("BabyAngry");

        yield return new WaitForSeconds(Random.Range(5, 10));

        isCoroutineReady = false;
    }

    public void TeleportBabyWithPinWeight()
    {
        puppetMaster.pinWeight = 1;
        BabyParent.position = new Vector3(9.401f, 2.64f, -6.391f);
        puppetMaster.GetComponent<PuppetMaster>().enabled = false;
        puppetMaster.GetComponent<PuppetMaster>().enabled = true;
        BabyParent.Rotate(new Vector3(0, 220, 0));
    }

    public void UnpinBabyWeight()
    {
        puppetMaster.pinWeight = 0;
        BabyParent.Rotate(new Vector3(0, -220, 0));
    }

    public bool Inspect()
    {
        if (BabyisLookingPlayer == true && playerCharacter.currentInput != new Vector2(0, 0))
            return true;
        else
            return false;
    }
}
