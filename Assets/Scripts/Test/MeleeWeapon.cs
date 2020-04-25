using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy e = collision.GetComponent<Enemy>();
        if (e != null)
        {
            e.ChangeHealth(-1);
        }
        destructibleObject obj = collision.GetComponent<destructibleObject>();
        if (obj != null)
        {
            obj.changeHealth(-1);
        }
    }
}
