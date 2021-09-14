using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBrush : GrabbableObject
{
    public GameObject drawerPrefab;

    public Transform paintBrushTip;
    public PaintTip paintTip;


    private GameObject drawer;

    private void Start()
    {
        
    }

    public override void OnTriggerStart()
    {
        if (paintTip.isColourSelected())
        {
            drawer = Instantiate(drawerPrefab, paintBrushTip.position, paintBrushTip.rotation);
            drawer.GetComponent<TrailRenderer>().material = paintTip.GetComponent<Renderer>().material;
        }

       
    }

    public override void OnTrigger()
    {
       if(drawer != null)
        {
            drawer.transform.position = paintBrushTip.transform.position;
        }
    }

    public override void OnTriggerEnd()
    {
        if(drawer != null)
        {
            drawer.transform.position = drawer.transform.position;
            drawer = null;
        }
    }

    public override void OnDropped()
    {
        base.OnDropped();
        paintTip.ResetPaint();
    }

}
