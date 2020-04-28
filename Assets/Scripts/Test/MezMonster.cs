using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MezMonster : Enemy
{    
    // Update is called once per frame
    protected override void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        base.Update();
    }
}
