using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class froggy : Enemy
{
    public bool facing;
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

                animator.SetTrigger("Attacking");
                nextAttackReady = false;

                int a = Random.Range(0, attackList.Count);
                Attack attack = attackList[a];
                while (attackList.Count > 1 && attack == currentAttack)
                {
                    int b = Random.Range(0, attackList.Count);
                    attack = attackList[b];
                }
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
            //animator.SetFloat("RubyX", lookDirection.x);

        }
        if (facing)
        {
            animator.SetFloat("Facing", 1);
        }
        else
        {
            animator.SetFloat("Facing", -1);
        }
    }
}

