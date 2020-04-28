using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMelee : Attack
{
    public int numberOfMelee = 1;
    public float delayMelee;
    public float dashTime;
    public GameObject hitEffect;
    public float size;

    LineRenderer lineRenderer;
    Animator animator;
    
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    override public IEnumerator Perform(Enemy enemyScript)
    {
        yield return new WaitForSeconds(1);
        if (animator == null)
            animator = enemyScript.GetComponent<Animator>();

        Vector2 playerPos = enemyScript.player.transform.position;
        Vector2 enemyPos = enemyScript.transform.position;
        Vector2 directionToPlayer = (playerPos - enemyPos).normalized;

        animator.SetFloat("Move X", directionToPlayer.x);

        lineRenderer.enabled = true;

        lineRenderer.SetPosition(0, new Vector3(enemyPos.x, enemyPos.y, -1f));
        lineRenderer.SetPosition(1, new Vector3(playerPos.x - directionToPlayer.x, playerPos.y - directionToPlayer.y, -1f));

        yield return new WaitForSeconds(delayMelee);

        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / dashTime;

            Vector2 pos = Vector3.Lerp(enemyPos, playerPos - directionToPlayer, t);
            enemyScript.GetComponent<Rigidbody2D>().MovePosition(pos);

            animator.SetFloat("Speed", 1-t);
            yield return null;
        }


        lineRenderer.enabled = false;

        animator.SetTrigger("Attack");
        for (int i = 0; i < numberOfMelee; i++)
        {
            yield return new WaitForSeconds(delayMelee);

            playerPos = enemyScript.player.transform.position;
            enemyPos = enemyScript.transform.position;
            directionToPlayer = (playerPos - enemyPos).normalized;

            GameObject effect = Instantiate(hitEffect, enemyPos + directionToPlayer,Quaternion.identity);
            effect.transform.localScale *= size;
            Collider2D[] damage = Physics2D.OverlapCircleAll(enemyPos + directionToPlayer, size / 2);

            for (int j = 0; j < damage.Length; j++)
            {
                RubyController controller = damage[j].GetComponent<RubyController>();
                if (controller != null)
                {
                    controller.ChangeHealth(-1);
                }
            }

            t = 0;
            while (t < 1)
            {
                t += Time.deltaTime / delayMelee;
                enemyScript.transform.position = Vector3.Lerp(enemyPos, enemyPos + directionToPlayer, t);
                yield return null;
            }

            animator.SetFloat("Move X", directionToPlayer.x);
        }

        yield return base.Perform(enemyScript);
    }

}
