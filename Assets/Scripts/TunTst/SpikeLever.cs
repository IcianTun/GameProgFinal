using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeLever : NonPlayerCharacter
{

    public Sprite left;
    public Sprite right;

    public List<SpikeBlockage> spikes;
    public Sprite currentSprite;

    protected override void Start()
    {

    }

    protected override void Update()
    {

    }

    public override void DisplayDialog()
    {
        foreach(SpikeBlockage spike in spikes)
        {
            spike.Toggle();
        }


    }
}
