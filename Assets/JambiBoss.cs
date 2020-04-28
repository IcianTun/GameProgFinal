using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JambiBoss : Enemy
{
    public GameObject projectilePrefab;
    private int state = 0;
    private int lastHealth = 20;
    // Start is called before the first frame update
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (player != null)
        {
            Vector2 playerPos = player.GetComponent<Rigidbody2D>().position;
            Vector2 position = rigidbody2d.position;

            float horizontal = playerPos.x - position.x;
            float vertical = playerPos.y - position.y;

            lookDirection = new Vector2(horizontal, vertical).normalized;
            Debug.Log("dir = " + lookDirection.x);
            animator.SetFloat("RubyX", lookDirection.x);

        }
        if(state != checkHealth())
        {
            state = checkHealth();
            if (state%4 == 0)
            {
                Vector2 position = rigidbody2d.position;
                Vector2 move = new Vector2(0, 9);
                position = position + move;
                rigidbody2d.MovePosition(position);
            }
            if (state % 4 == 1)
            {
                Vector2 position = rigidbody2d.position;
                Vector2 move = new Vector2(12, 0);
                position = position + move;
                rigidbody2d.MovePosition(position);
            }

            if (state % 4 == 2)
            {
                Vector2 position = rigidbody2d.position;
                Vector2 move = new Vector2(0, -9);
                position = position + move;
                rigidbody2d.MovePosition(position);
            }

            if (state % 4 == 3)
            {
                Vector2 position = rigidbody2d.position;
                Vector2 move = new Vector2(-12, 0);
                position = position + move;
                rigidbody2d.MovePosition(position);
            }

        }
    }

    private void Move()
    {

    }

    void Attack()
    {
        Debug.Log(lookDirection);
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        ProjectileEnemy projectile = projectileObject.GetComponent<ProjectileEnemy>();
        projectile.Launch(lookDirection, 300);
    }
    private int checkHealth()
    {
        if(lastHealth != health)
        {
            state = (state + 1) % 4;
            lastHealth = health;

        }
        return state;
    }
}
