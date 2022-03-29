using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offAllColliders : MonoBehaviour
{
    public GameObject baby;
    public Collider[] _ragdollColliders;
    public Rigidbody[] _rigidBody;
    public Animator BabyAnimator;
    // Start is called before the first frame update
    void Start()
    {
        baby = this.gameObject;
        _ragdollColliders = baby.GetComponentsInChildren<Collider>();
        _rigidBody = baby.GetComponentsInChildren<Rigidbody>();
        offRagdoll();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            onRagdoll();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            offRagdoll();
        }
    }
    public void offRagdoll()
    {
        foreach (Collider item in _ragdollColliders)
        {
            item.enabled = false;
        }
        foreach (Rigidbody item in _rigidBody)
        {
            item.isKinematic = true; //in order to avoid phisical Forces
        }
        BabyAnimator.enabled = true;
   }
    public void onRagdoll()
    {
        foreach (Collider item in _ragdollColliders)
        {
            item.enabled = true; 
        }
        foreach (Rigidbody item in _rigidBody)
        {
            item.isKinematic = false; //in order to enable phisical Forces
        }
        BabyAnimator.enabled = false;
    }
    //use these funtions
    //when you want to use animator disable ragdoll => offRagdoll()
    //when you want to use ragdoll disable animator => onRagdoll()
    //accees animator of the baby and enable disable it on pickup and putdown
}
