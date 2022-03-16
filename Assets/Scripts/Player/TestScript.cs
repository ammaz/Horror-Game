using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    public FixedJoint Baby;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickDrop()
    {
        if (Baby.connectedMassScale == 0)
        {
            Baby.connectedMassScale = 2;
        }
        else
        {
            Baby.connectedMassScale = 0;
        }
    }

}
