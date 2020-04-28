using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippityFlipWallDamageZone : MonoBehaviour
{

    public int damage = 1;
    private bool disabled = false;
    

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!disabled)
        {
            RubyController t = collision.collider.GetComponent<RubyController>();
            if (t != null)
            {
                t.ChangeHealth(-damage);
            }
        }

    }

    public void DisableDamageZone()
    {
        disabled = true;
    }
}
