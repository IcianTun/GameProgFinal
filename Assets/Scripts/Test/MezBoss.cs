using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MezBoss : Enemy
{
    public GameObject winningZone;

    // Update is called once per frame
    protected override void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        base.Update();
    }

    private void OnDestroy()
    {
        winningZone.SetActive(true);
    }
}
