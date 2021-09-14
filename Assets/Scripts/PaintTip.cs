using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintTip : MonoBehaviour
{
    private bool _isColourSelected;
    private Material defaultMaterial;


    public bool isColourSelected()
    {
        return _isColourSelected;
    }

    // Start is called before the first frame update
    void Start()
    {
        defaultMaterial = GetComponent<Renderer>().material;
        _isColourSelected = false;
    }

    public void ResetPaint()
    {
        _isColourSelected = false;
        GetComponent<Renderer>().material = defaultMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("paint"))
        {
            GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;
            _isColourSelected = true;
        }
    }
}
