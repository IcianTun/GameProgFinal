using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tackle : Attack
{
    public int numberOfCharge;
    public float delayStartTime;
    public float dashTime;

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
        for (int i = 0; i < numberOfCharge; i++)
        {
            Vector2 playerPos = enemyScript.player.transform.position;
            Vector2 enemyPos = enemyScript.transform.position;
            Vector2 directionToPlayer = (playerPos - enemyPos).normalized;

            animator.SetFloat("Move X", directionToPlayer.x);

            lineRenderer.enabled = true;

            lineRenderer.SetPosition(0, new Vector3(enemyPos.x, enemyPos.y, -1f));
            lineRenderer.SetPosition(1, new Vector3(playerPos.x, playerPos.y, -1f));

            yield return new WaitForSeconds(delayStartTime);

            float t = 0f;
            while (t < 1)
            {
                t += Time.deltaTime / dashTime;

                Vector2 pos = Vector3.Lerp(enemyPos, playerPos - directionToPlayer, t);
                enemyScript.GetComponent<Rigidbody2D>().MovePosition(pos);

                animator.SetFloat("Speed", 1 - t);
                yield return null;
            }

            lineRenderer.enabled = false;
        }

        yield return base.Perform(enemyScript);
    }
}
