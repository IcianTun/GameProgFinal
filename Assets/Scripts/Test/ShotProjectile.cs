using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotProjectile : Attack
{
    public GameObject projectilePrefab;

    Animator animator;

    override public IEnumerator Perform(Enemy enemyScript)
    {
        if (animator == null)
            animator = enemyScript.GetComponent<Animator>();

        Vector3 directionToPlayer = (enemyScript.player.transform.position - enemyScript.transform.position).normalized;
        animator.SetFloat("Move X", directionToPlayer.x);
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(1);

        GameObject projectileObject = Instantiate(projectilePrefab, enemyScript.transform.position + directionToPlayer, Quaternion.identity);
        ProjectileEnemy projectile = projectileObject.GetComponent<ProjectileEnemy>();

        projectile.Launch(directionToPlayer, 300);

        yield return base.Perform(enemyScript);
    }

}
