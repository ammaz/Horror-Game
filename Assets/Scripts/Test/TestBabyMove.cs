using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBabyMove : MonoBehaviour
{
    public FixedJoint TestBabyPickPoint;

    public void PickDropTestBaby()
    {
        if (TestBabyPickPoint.connectedMassScale == 5)
        {
            TestBabyPickPoint.connectedMassScale = 0;
        }
        else
        {
            TestBabyPickPoint.connectedMassScale = 5;
        }
    }
}
