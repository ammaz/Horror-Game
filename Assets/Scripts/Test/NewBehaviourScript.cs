using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Collider MainCollider;
    public Collider[] AllColliders;

    private void Awake()
    {
        MainCollider = GetComponent<Collider>();
        AllColliders = GetComponentsInChildren<Collider>(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            DoRagDoll(true);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            DoRagDoll(false);
        }
    }

    public void DoRagDoll(bool isRagDoll)
    {
        foreach (var col in AllColliders)
            col.enabled = isRagDoll;
        MainCollider.enabled = !isRagDoll;
        GetComponent<Rigidbody>().useGravity = !isRagDoll;
        GetComponent<Animator>().enabled = !isRagDoll;
    }
}
