using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidHead : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    private float valueY;

    private void Update()
    {
        valueY = Mathf.Clamp(target.position.y, 0f, 1.5f);
        transform.LookAt(new Vector3(target.position.x, valueY, target.position.z));
    }
}
