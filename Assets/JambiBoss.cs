﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JambiBoss : Enemy
{
    public GameObject projectilePrefab;

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
}