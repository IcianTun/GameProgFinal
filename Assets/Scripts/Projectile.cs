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
        if (Vector2.Distance(awakePos,current) > 20)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy e = collision.GetComponent<Enemy>();
        if (e != null)
        {
            e.ChangeHealth(-1);
        }
        EnemyRobotController e2 = collision.GetComponent<EnemyRobotController>();
        if (e2 != null)
        {
            e2.Fix();
        }
        Destroy(gameObject);
    }
}
