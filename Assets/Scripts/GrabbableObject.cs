using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    
    public Color hovererdColour;

    private Material material;
    private Rigidbody body;
    private Color tempColour;


    // Start is called before the first frame update
    void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        body = GetComponent<Rigidbody>();
        tempColour = material.color;
    }

    public virtual void OnHoverStart()
    {
        material.color = hovererdColour;
    }

    public virtual void OnHoverEnd()
    {
        material.color = tempColour;
    }

    public virtual void OnGrabbed(Grabber hand)
    {
        transform.SetParent(hand.transform);
        body.useGravity = false;
        body.isKinematic = true;
    }

    public virtual void OnDropped()
    {
        transform.SetParent(null);
        body.useGravity = true;
        body.isKinematic = false;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
