using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyAnim : MonoBehaviour
{
    private Animator BabyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        BabyAnimator = GetComponent<Animator>();
    }

    public void BabyCry()
    {
        BabyAnimator.SetBool("Horror", false);
        BabyAnimator.SetBool("Happy", false);
        BabyAnimator.SetBool("Idle", false);
        BabyAnimator.SetBool("Pick", false);
        BabyAnimator.SetBool("Sit", false);
        BabyAnimator.SetBool("Cry", true);
    }

    public void BabyPickUp()
    {
        BabyAnimator.SetBool("Horror", false);
        BabyAnimator.SetBool("Happy", false);
        BabyAnimator.SetBool("Idle", false);
        BabyAnimator.SetBool("Cry", false);
        BabyAnimator.SetBool("Sit", false);
        BabyAnimator.SetBool("Pick", true);  
    }

    public void BabyIdle()
    {
        BabyAnimator.SetBool("Horror", false);
        BabyAnimator.SetBool("Happy", false);
        BabyAnimator.SetBool("Pick", false);
        BabyAnimator.SetBool("Cry", false);
        BabyAnimator.SetBool("Sit", false);
        BabyAnimator.SetBool("Idle", true);
    }

    public void BabySit()
    {
        BabyAnimator.SetBool("Horror", false);
        BabyAnimator.SetBool("Happy", false);
        BabyAnimator.SetBool("Pick", false);
        BabyAnimator.SetBool("Cry", false);
        BabyAnimator.SetBool("Idle", false);
        BabyAnimator.SetBool("Sit", true);
    }

    public void BabyHappy()
    {
        BabyAnimator.SetBool("Horror", false);
        BabyAnimator.SetBool("Sit", false);
        BabyAnimator.SetBool("Pick", false);
        BabyAnimator.SetBool("Cry", false);
        BabyAnimator.SetBool("Idle", false);
        BabyAnimator.SetBool("Happy", true);
    }

    public void BabyHorror()
    {
        BabyAnimator.SetBool("Sit", false);
        BabyAnimator.SetBool("Happy", false);
        BabyAnimator.SetBool("Pick", false);
        BabyAnimator.SetBool("Cry", false);
        BabyAnimator.SetBool("Idle", false);
        BabyAnimator.SetBool("Horror", true);
    }
}
