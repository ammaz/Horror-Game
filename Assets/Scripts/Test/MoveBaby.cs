using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBaby : MonoBehaviour
{
    public Transform BabyPickPoint;
    public GameObject AnimBaby;
    public Rigidbody Baby;

    private bool BabyisPicked;

    public Transform rotationObject;


    // Start is called before the first frame update
    void Start()
    {
        BabyisPicked = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.J))
        {
            //BabyisPicked = true;
            PickUpBaby(true);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            //BabyisPicked = false;
            PickUpBaby(false);
        }*/
    }

    public void PickUpBaby()
    {
        if (BabyisPicked)
        {
            AnimBaby.transform.parent = BabyPickPoint;
            AnimBaby.transform.position = BabyPickPoint.position;
            AnimBaby.transform.rotation = rotationObject.rotation;
            Baby.isKinematic = true; 
        }
        else
        {
            AnimBaby.transform.parent = null;
            Baby.isKinematic = false;
            Baby.AddForce(transform.forward * 100f);
            AnimBaby.transform.rotation = rotationObject.rotation;

            Baby.drag = 2;
        }
        BabyisPicked = !BabyisPicked;  
    }
}
