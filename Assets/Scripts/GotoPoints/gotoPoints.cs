using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gotoPoints : MonoBehaviour
{
    public string objName;
    public static string pointName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pointName = objName;
        }
    }
}
