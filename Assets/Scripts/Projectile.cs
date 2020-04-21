using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    Vector2 awakePos;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        awakePos = new Vector2(transform.position.x, transform.position.y);
    }

    void Update()
    {
        Vector2 current = new Vector2(transform.position.x, transform.position.y);
        //if (transform.position.magnitude > 1000.0f)
        if (Vector2.Distance(awakePos,current) > 10)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if (e != null)
        {
            e.Fix();
        }

        Destroy(gameObject);


        //test
        Enemy t = other.collider.GetComponent<Enemy>();
        if (t != null)
        {
            t.ChangeHealth(-1);
        }
        Debug.Log(other.gameObject);
    }
}
