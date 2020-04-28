using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBlockage : MonoBehaviour
{

    Animator animator;
    bool activated;

    BoxCollider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {
        activated = true;
        animator = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
    }
    

    public void Toggle()
    {
        activated = !activated;
        animator.SetBool("Activated", activated);
        myCollider.enabled = activated;
    }

}
