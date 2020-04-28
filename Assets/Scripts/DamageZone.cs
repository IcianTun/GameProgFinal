using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public bool enable = true;
    private Sprite sprite;
    private SpriteRenderer sr;
    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        sprite = sr.sprite;
        if (!enable) {
            sr.sprite = null;
        }
    }

    private void Update()
    {
        if (!enable) {
            sr.sprite = null;
        } else
        {
            sr.sprite = sprite;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (enable)
        {

            RubyController controller = other.GetComponent<RubyController>();

            if (controller != null)
            {
                controller.ChangeHealth(-1);
            }

            Hostage hostage = other.GetComponent<Hostage>();

            if (hostage != null)
            {
                hostage.ChangeHealth(-1);
            }
        }

    }
}
