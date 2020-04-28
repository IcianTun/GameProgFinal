using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MezBoss : Enemy
{
    public GameObject winningZone;

    // Update is called once per frame
    protected override void Update()
    {
        if (currentHealth == 0)
        {
            winningZone.SetActive(true);
        }
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        base.Update();
    }
}
