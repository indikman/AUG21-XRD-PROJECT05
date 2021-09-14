using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTest : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;

    public Transform pointD;
    public Transform pointE;
    public Transform pointX;




    float timer = 0f;

    
    void Update()
    {

        timer += Time.deltaTime;
        if(timer > 1)
        {
            timer = 0;
        }

        pointC.position = Vector3.Lerp(pointA.position, pointB.position, timer);
        pointE.position = Vector3.Lerp(pointB.position, pointD.position, timer);



        pointX.position = Vector3.Lerp(pointC.position, pointE.position, timer);

    }
}
