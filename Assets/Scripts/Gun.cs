using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : GrabbableObject
{
    public Transform gunTip;
    public GameObject bullet;

    public float bulletSpeed;

    void Start()
    {
        
    }


    public override void OnTriggerStart()
    {
        base.OnTriggerStart();

        var newBullet = Instantiate(bullet, gunTip.position, gunTip.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(gunTip.forward * bulletSpeed, ForceMode.Impulse);


        Destroy(newBullet, 5);
    }


}
