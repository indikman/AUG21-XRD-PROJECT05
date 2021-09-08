using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRLocomotion : MonoBehaviour
{
    public string verticalAxis;
    public string horizontalAxis;
    public string triggerButton;

    public Transform XRRig;
    public Transform headCamera;

    public bool CanSmoothMove;
    public bool CanSmoothRotate;
    public float movementSpeed = 10f;
    public float smoothRotateSpeed = 10f;

    public Transform reticle;

    private LineRenderer line;

    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var verticalVal = Input.GetAxis(verticalAxis);
        var horizontalVal = Input.GetAxis(horizontalAxis);

        //Debug.Log("Vertical : " + verticalVal + "\nHorizontal : " + horizontalVal);
        if (CanSmoothMove)
            SmoothMove(verticalVal);

        if(CanSmoothRotate)
            SmoothRotate(horizontalVal);

        Teleport();

    }


    void SmoothMove(float val)
    {
        Vector3 lookDirection = new Vector3(headCamera.forward.x, 0, headCamera.forward.z);
        lookDirection.Normalize();

        XRRig.position += Time.deltaTime * lookDirection * val * -1 * movementSpeed;
    }

    void SmoothRotate(float val)
    {
        XRRig.Rotate(Vector3.up, smoothRotateSpeed * val * Time.deltaTime);
    }


    void Teleport()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            bool validTarget = hit.collider.CompareTag("teleportable");

            line.enabled = true;
            reticle.gameObject.SetActive(true);
            reticle.position = hit.point;
            reticle.transform.up = hit.normal;


            if (validTarget)
            {
                line.startColor = Color.green;
                line.endColor = Color.blue;
            }
            else
            {
                line.startColor = Color.red;
                line.endColor = Color.yellow;
            }

            line.SetPosition(0, transform.position);
            line.SetPosition(1, hit.point);

            if (validTarget && Input.GetButtonDown(triggerButton))
            {
                XRRig.position = hit.point;
            }

        }
        else
        {
            reticle.gameObject.SetActive(false);
            line.enabled = false;
        }
    }
}
