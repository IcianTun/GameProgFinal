using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    bool alreadyHit;
    public Color activatedColor;

    private void Awake()
    {
        alreadyHit = false;
    }

    public void DelayLaunch(Vector2 direction,float delay)
    {
        StartCoroutine(LaunchRoutine(direction,delay));
    }

    private IEnumerator LaunchRoutine(Vector2 direction,float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().color = activatedColor;
        StartCoroutine(DestroyAfter(1));
    }


    IEnumerator DestroyAfter(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!alreadyHit)
        {
            RubyController t = collision.GetComponent<RubyController>();
            if (t != null)
            {
                t.ChangeHealth(-1);
                alreadyHit = true;
            }
        }
    }

}
