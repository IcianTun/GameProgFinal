using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeLever : NonPlayerCharacter
{

    public Sprite left;
    public Sprite right;

    public List<SpikeBlockage> spikes;

    SpriteRenderer spriteRenderer;

    private bool isLeft;

    protected override void Start()
    {
        isLeft = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (isLeft)
        {
            spriteRenderer.sprite = right;
        }
        else
        {
            spriteRenderer.sprite = left;
        }
        isLeft = !isLeft;

    }
}
