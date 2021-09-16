using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverHandle : GrabbableObject
{
    public Transform leverHandlePoint;
    public Rigidbody lever;

    private Rigidbody body;
    private FixedJoint joint;

    private bool leverGrabbed;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        leverGrabbed = false;
    }

    public override void OnHoverStart()
    {
       // base.OnHoverStart();
    }

    public override void OnHoverEnd()
    {
       // base.OnHoverEnd();
    }


    public override void OnGrabbed(Grabber hand)
    {
        base.OnGrabbed(hand);
        joint = gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = lever;

        leverGrabbed = true;

    }

    public override void OnDropped()
    {
        base.OnDropped();

        joint.connectedBody = null;
        Destroy(joint);

        body.isKinematic = true;
        body.useGravity = false;

        leverGrabbed = false;
        
    }

    private void Update()
    {
        if (!leverGrabbed)
        {
            transform.position = leverHandlePoint.position;
        }
    }
}
