using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }

        Hostage hostage = other.GetComponent<Hostage>();

        if (hostage!= null)
        {
            hostage.ChangeHealth(-1);
        }

    }
}
