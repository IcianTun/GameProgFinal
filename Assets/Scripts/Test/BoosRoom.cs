using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosRoom : RoomController
{
    public Transform hostageLocation;

    Hostage hostage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        hostage = collision.GetComponent<Hostage>();
        if (hostage != null)
        {
            hostage.SetDestination(hostageLocation);
        }
    }
}
