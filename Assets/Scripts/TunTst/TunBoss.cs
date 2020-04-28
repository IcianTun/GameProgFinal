using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunBoss : Enemy
{
    [Header("TunBOss")]
    public Transform center;
    public TunBossRoomController roomController;

    protected override void Unlock()
    {
        roomController.Lock = false;
    }

}
