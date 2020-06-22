using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MezBoss : Enemy
{
    public GameObject winningZone;

    public AudioClip collectSoundClip;
    public RubyController ruby;


    // Update is called once per frame
    protected override void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        base.Update();
    }

    override protected void Unlock()
    {
        ruby.PlaySound(collectSoundClip);
        winningZone.SetActive(true);

    }

}
