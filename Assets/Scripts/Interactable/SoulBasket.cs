using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBasket : MonoBehaviour
{
    public static int countSouls;
    // Start is called before the first frame update
    void Start()
    {
        countSouls = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name=="Soul")
        {
            if (other.GetComponent<Rigidbody>().useGravity == true)
            {
                countSouls++;
            }
        }
    }
}
