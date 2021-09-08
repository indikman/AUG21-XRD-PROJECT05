using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public Animator handAnimator;

    public string gripButton;

    private GrabbableObject hoveredObject;
    private GrabbableObject grabbedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var grabbable = other.GetComponent<GrabbableObject>();
        if(grabbable != null)
        {
            hoveredObject = grabbable;
            hoveredObject.OnHoverStart();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var grabbable = other.GetComponent<GrabbableObject>();
        if (grabbable != null && grabbable == hoveredObject)
        {

            hoveredObject.OnHoverEnd();
            hoveredObject = null;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(gripButton))
        {
            handAnimator.SetBool("Gripped", true);

            if(hoveredObject != null)
            {
                hoveredObject.OnGrabbed(this);

                grabbedObject = hoveredObject;
                hoveredObject = null;
            }
        }

        if (Input.GetButtonUp(gripButton))
        {
            handAnimator.SetBool("Gripped", false);

            if(grabbedObject != null)
            {
                grabbedObject.OnDropped();
                grabbedObject = null;
            }
        }
    }
}
