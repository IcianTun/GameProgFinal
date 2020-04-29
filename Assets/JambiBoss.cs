using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JambiBoss : Enemy
{
    public GameObject projectilePrefab;
    private int state = 0;
    private int lastHealth = 12;

    public key key;
    private int attackNumber = 0;
    // Start is called before the first frame update
    // Update is called once per frame
    protected override void Update()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (player != null)
        {
            tstVector2PlayerPos = player.transform.position;
            if (nextAttackReady && attackList.Count > 0)
            {
                nextAttackReady = false;


                Attack attack = attackList[attackNumber];
                currentAttack = attack;
                StartCoroutine(attack.Perform(this));
                //waitDelayForNextAttack = attack.totalSubAttacksExecuteTime + attack.delayAfterAttack;
            }

            Vector2 playerPos = player.GetComponent<Rigidbody2D>().position;
            Vector2 position = rigidbody2d.position;

            float horizontal = playerPos.x - position.x;
            float vertical = playerPos.y - position.y;

            lookDirection = new Vector2(horizontal, vertical).normalized;
        }


        if (player != null)
        {
            Vector2 playerPos = player.GetComponent<Rigidbody2D>().position;
            Vector2 position = rigidbody2d.position;

            float horizontal = playerPos.x - position.x;
            float vertical = playerPos.y - position.y;

            lookDirection = new Vector2(horizontal, vertical).normalized;
            animator.SetFloat("RubyX", lookDirection.x);

        }
        if(state != checkHealth())
        {
            state = checkHealth();
            if (state%4 == 0)
            {
                Move(0, 9);
            }
            if (state % 4 == 1)
            {
                Move(12, 0);
            }

            if (state % 4 == 2)
            {
                Move(0, -9);
            }

            if (state % 4 == 3)
            {
                Move(-12, 0);
            }

        }
    }

    private void Move(float x, float y)
    {
        Vector2 position = rigidbody2d.position;
        Vector2 move = new Vector2(x, y);
        position = position + move;
        MoveToPosition(position, 0.01f, 0);

    }

    void Attack()
    {
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
            if (health <= 4)
            {
                attackNumber = 3;
            }
            else if (health <= 8)
            {
                attackNumber = 2;
            }
            else if (health <= 10)
            {
                attackNumber = 1;
            }
            else if (health <= 12)
            {
                attackNumber = 0;
            }
        }
        return state;
    }
    protected override void Unlock()
    {
        key.setActive(false);
        key.unLock();

    }

}
