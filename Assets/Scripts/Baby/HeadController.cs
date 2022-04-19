using UnityEngine;
using System;
using System.Collections;

public class HeadController : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    private float valueY;

    public FieldOfView FOV;

    public FirstPersonController isBabyPicked;

    private void Update()
    {
        valueY = Mathf.Clamp(target.position.y,0f,1.5f);

        if(FOV.canSeePlayer && isBabyPicked.Baby.connectedMassScale==0)
            transform.LookAt(new Vector3(target.position.x, valueY, target.position.z));

        /*rotationX = Mathf.Clamp(rotationX, minRotationX, maxRotationX);
        rotationY += Input.GetAxis("Horizontal" * Time.deltaTime;
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);*/

        /*Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);*/
    }
}