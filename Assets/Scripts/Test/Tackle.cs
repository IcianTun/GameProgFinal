using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tackle : Attack
{
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
        animator = enemyScript.GetComponent<Animator>();

        Vector2 playerPos = enemyScript.player.transform.position;
        Vector2 enemyPos = enemyScript.transform.position;
        Vector2 directionToPlayer = (playerPos - enemyPos).normalized;

        animator.SetFloat("Move X", directionToPlayer.x);
        animator.SetFloat("Move Y", directionToPlayer.y);

        lineRenderer.enabled = true;

        lineRenderer.SetPosition(0, new Vector3(enemyPos.x,enemyPos.y,-1f));
        lineRenderer.SetPosition(1, new Vector3(playerPos.x, playerPos.y, -1f));

        yield return new WaitForSeconds(delayStartTime);

        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / dashTime;
            enemyScript.transform.position = Vector3.Lerp(enemyPos, playerPos, t);
            yield return null;
        }

        lineRenderer.enabled = false;
        //yield return new WaitForSeconds(dashTime);

        //GameObject projectileObject = Instantiate(projectilePrefab, enemyScript.transform.position + directionToPlayer, Quaternion.identity, enemyScript.transform);
        //ProjectileEnemy projectile = projectileObject.GetComponent<ProjectileEnemy>();

        //projectile.Launch(directionToPlayer, 300);
        yield return base.Perform(enemyScript);
    }

}
