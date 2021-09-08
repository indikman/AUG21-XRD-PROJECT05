using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketObject : ThrowableObject
{

    bool isWithinaSocket = false;
    private Transform nearbySocket;

    public override void OnGrabbed(Grabber hand)
    {
        base.OnGrabbed(hand);
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    public override void OnDropped()
    {
        base.OnDropped();

        if (isWithinaSocket && nearbySocket != null)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<Rigidbody>().useGravity = false;

            transform.position = nearbySocket.position;
            transform.rotation = nearbySocket.rotation;
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Socket"))
        {
            isWithinaSocket = true;
            nearbySocket = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Socket"))
        {
            isWithinaSocket = false;
            nearbySocket = null;
        }
    }
}
