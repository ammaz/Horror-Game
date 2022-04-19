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
                countToys++;
            }
        }
    }
}
