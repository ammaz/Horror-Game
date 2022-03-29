using UnityEngine;
using System;
using System.Collections;

public class HeadController : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;

    private void Update()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation,rotation,speed*Time.deltaTime);
    }
}