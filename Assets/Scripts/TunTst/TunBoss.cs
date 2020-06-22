using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunBoss : Enemy
{
    [Header("TunBOss")]
    public Transform center;
    public TunBossRoomController roomController;


    public AudioClip collectSoundClip;
    public RubyController ruby;


    protected override void Unlock()
    {
        ruby.PlaySound(collectSoundClip);
        roomController.Lock = false;
    }

}
