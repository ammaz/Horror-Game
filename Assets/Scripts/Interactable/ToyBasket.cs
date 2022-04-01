using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyBasket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
                Destroy(other.gameObject,0.1f);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Toys"))
        {
            if (other.GetComponent<Rigidbody>().useGravity == true)
            {
                Destroy(other.gameObject, 0.1f);
            }
        }
    }
}
