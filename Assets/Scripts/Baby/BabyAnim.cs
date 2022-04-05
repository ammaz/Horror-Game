using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyAnim : MonoBehaviour
{

    public Animator BabyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BabyCry()
    {
        if (BabyAnimator.GetBool("Idle"))
        {
            BabyAnimator.SetBool("Idle",false);
            BabyAnimator.SetBool("Cry",true);
        }

    }
}
