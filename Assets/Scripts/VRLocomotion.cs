using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRLocomotion : MonoBehaviour
{
    [Header("Input")]
    public hands hand;
    

    [Header("Base XR")]
    public Transform XRRig;
    public Transform headCamera;

    [Header("Movement")]
    public bool CanSmoothMove;
    public bool CanSmoothRotate;
    public float movementSpeed = 10f;
    public float smoothRotateSpeed = 10f;

    [Header("Line")]
    public Vector3 curveHeight;
    public int lineResolution = 20;
    public Transform reticle;

    [Header("Fader")]
    public RawImage fader;
    public float fadeTime = 0.2f;

    
    //-----------------------------------

    private LineRenderer line;
    private RaycastHit hit;
    private bool teleportLock;
    private string verticalAxis;
    private string horizontalAxis;
    private string triggerButton;


    public enum hands
    {
        Left,
        Right
    }

    private void Awake()
    {
        verticalAxis = "XRI_" + hand +  "_Primary2DAxis_Vertical";
        horizontalAxis = "XRI_" + hand + "_Primary2DAxis_Horizontal";
        triggerButton = "XRI_" + hand + "_TriggerButton";
    }


    // Start is called before the first frame update
    void Start()
    {

        line = GetComponent<LineRenderer>();
        line.positionCount = lineResolution;

        fader.color = Color.clear;
        teleportLock = false;
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

            Vector3 startPoint = transform.position;
            Vector3 endPoint = hit.point;
            Vector3 midPoint = ((endPoint - startPoint) / 2) + startPoint; // calculate the midpoint
            midPoint += curveHeight;



            Vector3 desiredPosition = endPoint - reticle.transform.position;
            Vector3 smoothVectoDesired = (desiredPosition / 0.2f) * Time.deltaTime;
            Vector3 reticleEndpoint = reticle.transform.position + smoothVectoDesired;

            reticle.position = reticleEndpoint;

            reticle.transform.up = hit.normal;


            // Straght Line with 2 points
            //line.SetPosition(0, transform.position);
            //line.SetPosition(1, hit.point);


            // Set line positions based on the lerp values
            for (int i=0; i < lineResolution; i++)
            {
                float t = i / (float)lineResolution;

                Vector3 startToMid = Vector3.Lerp(startPoint, midPoint, t);
                Vector3 midToEnd = Vector3.Lerp(midPoint, reticleEndpoint, t);

                //curve position
                Vector3 curvePosition = Vector3.Lerp(startToMid, midToEnd, t);

                line.SetPosition(i, curvePosition);

            }



            if (!teleportLock && validTarget && Input.GetButtonDown(triggerButton))
            {
                StartCoroutine(FadeTeleport(hit.point));
            }

        }
        else
        {
            reticle.gameObject.SetActive(false);
            line.enabled = false;
        }
    }


    private IEnumerator FadeTeleport(Vector3 newPosition)
    {
        teleportLock = true;

        float timer = 0f;
        
        // Fade in
        while(timer < fadeTime)
        {
            fader.color = Color.Lerp(Color.clear, Color.black, timer);
            yield return new WaitForEndOfFrame();

            timer += Time.deltaTime;
        }

        fader.color = Color.black;

        // Teleport
        XRRig.transform.position = newPosition;

        yield return new WaitForSeconds(fadeTime);

        timer = 0f;

        // Fade out
        while (timer < fadeTime)
        {
            fader.color = Color.Lerp(Color.black, Color.clear, timer);
            yield return new WaitForEndOfFrame();

            timer += Time.deltaTime;
        }

        fader.color = Color.clear;

        teleportLock = false;

    }

}
