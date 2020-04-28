using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeriesOfProjectile : Attack
{
    public GameObject projectilePrefab;
    public int numberOfProjectile = 1;
    public float delayStartTime;
    public float projectileOffset;
    public bool focus;

    List<GameObject> projectiles;
    Animator animator;

    override public IEnumerator Perform(Enemy enemyScript)
    {
        yield return new WaitForSeconds(1);
        projectiles = new List<GameObject>();

        if (animator == null)
            animator = enemyScript.GetComponent<Animator>();

        Vector2 playerPos = enemyScript.player.transform.position;
        Vector2 enemyPos = enemyScript.transform.position;
        Vector2 directionToPlayer = (playerPos - enemyPos).normalized;

        if (animator)
            animator.SetFloat("Move X", directionToPlayer.x);
        else
            Debug.Log("NO ANIMATOR NAJAAA");

        int space = numberOfProjectile - 1;
        for (int i = 0; i < numberOfProjectile; i++)
        {
            Vector2 axis = Vector2.Perpendicular(directionToPlayer).normalized;
            Vector2 offset = axis * projectileOffset * (i - (float)space / 2);
            GameObject projectileObject = Instantiate(projectilePrefab, enemyPos + directionToPlayer + offset, Quaternion.identity);


            ProjectileEnemy projectile = projectileObject.GetComponent<ProjectileEnemy>();
            Vector2 dir = directionToPlayer;
            if (focus)
            {
                Vector2 bulletPos = projectile.transform.position;
                dir = (playerPos - bulletPos).normalized;
            }
            projectile.DelayLaunch(dir, 600, delayStartTime);
        }

        yield return base.Perform(enemyScript);
    }

}
