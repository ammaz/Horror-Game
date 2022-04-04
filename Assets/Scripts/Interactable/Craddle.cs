using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craddle : MonoBehaviour
{
    public FirstPersonController player;
    public static bool BabyinCraddle;

    void Start()
    {
        BabyinCraddle = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cradle"))
        {
            if (player.Baby.connectedMassScale==0)
            {
                if (BabyinCraddle == false)
                    BabyinCraddle = true;
            }
        }
        else
        {
            if(BabyinCraddle==true)
                BabyinCraddle = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Cradle"))
        {
            if (player.Baby.connectedMassScale==0)
            {
                if (BabyinCraddle == false)
                    BabyinCraddle = true;
            }
        }
        else
        {
            if (BabyinCraddle == true)
                BabyinCraddle = false;
        }
    }
}
