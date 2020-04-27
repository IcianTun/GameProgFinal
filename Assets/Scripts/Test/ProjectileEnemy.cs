using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (transform.position.magnitude > 100.0f)
        {
            Destroy(gameObject);
        }
    }

    public void DelayLaunch(Vector2 direction, float force,float delay)
    {
        StartCoroutine(LaunchRoutine(direction, force, delay));
    }

    private IEnumerator LaunchRoutine(Vector2 direction, float force, float delay)
    {
        yield return new WaitForSeconds(delay);
        Launch(direction, force);
    }


    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool hit = false;
        RubyController t = collision.GetComponent<RubyController>();
        if (t != null)
        {
            t.ChangeHealth(-1);
            hit = true;
        }

        Hostage h = collision.GetComponent<Hostage>();
        if (h != null)
        {
            h.ChangeHealth(-1);
            hit = true;
        }
        if (hit)
        {
            Destroy(gameObject);
        }
    }
}
