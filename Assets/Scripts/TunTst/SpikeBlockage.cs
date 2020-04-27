using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBlockage : MonoBehaviour
{

    Animator animator;
    bool activated;

    // Start is called before the first frame update
    void Start()
    {
        activated = true;
        animator = GetComponent<Animator>();
    }
    

    public void Toggle()
    {
        animator.SetBool("Activated", activated);
        activated = !activated;

    }

}
