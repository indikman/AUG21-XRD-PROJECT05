using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : GrabbableObject
{
    public int velocitySamples = 10;
    public float throwBoost = 50;

    FixedJoint joint;

    private Queue<Vector3> previousVelocities = new Queue<Vector3>();
    private Vector3 previousPosition;


    public override void OnGrabbed(Grabber hand)
    {
        joint = gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = hand.GetComponent<Rigidbody>();
    }

    public override void OnDropped()
    {
        if(joint != null)
        {
            Destroy(joint);

            var averageVelocity = Vector3.zero;
            
            foreach(var velocity in previousVelocities)
            {
                averageVelocity += velocity;
            }

            averageVelocity /= previousVelocities.Count;

            GetComponent<Rigidbody>().velocity = averageVelocity * throwBoost;

        }
        
    }

    private void FixedUpdate()
    {
        // calculate the differce between position
        var velocity = transform.position - previousPosition;

        //remember the current position to be used in next iteration
        previousPosition = transform.position;

        //Add the calculated velocity / (position difference) in to our queue
        previousVelocities.Enqueue(velocity);


        // remove the first velocity if the size of the queue hits the limit
        if(previousVelocities.Count > 10)
        {
            // remove the oldest sample
            previousVelocities.Dequeue();
        }

    }

}
