using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunBoss : Enemy
{
    protected override void Start()
    {
        base.Initiate(10, 3.0f, 2.0f);
        base.Start();
    }


}
