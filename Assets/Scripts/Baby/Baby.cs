using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : MonoBehaviour
{
    public void BabyTPSit()
    {
        gameObject.transform.position = new Vector3(-7.042f, 0.849f, -7.13f);
    }

    public void BabyTP(float x,float y,float z)
    {
        gameObject.transform.position = new Vector3(x, y, z);
    }
}
