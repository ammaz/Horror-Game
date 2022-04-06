using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyBasket : MonoBehaviour
{
    public static int countToys;
    // Start is called before the first frame update
    void Start()
    {
        countToys = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Toys"))
        {
            if (other.GetComponent<Rigidbody>().useGravity == true)
            {
                //Destroy(other.gameObject,0.1f);
                countToys++;
            }
        }
    }
    /*
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Toys"))
        {
            if (other.GetComponent<Rigidbody>().useGravity == true)
            {
                //Destroy(other.gameObject, 0.1f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Toys"))
        {
            if (other.GetComponent<Rigidbody>().useGravity == true)
            {
                //Destroy(other.gameObject, 0.1f);
                //countToys++;
            }
        }
    }
    */
}
