using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCircle : Attack
{

    public GameObject orbBulletPrefab;
    public int numberOfProjectile = 5;
    public int repeatMoveTime = 2;

    public float bulletSpeed = 3f;


    override public IEnumerator Perform(Enemy enemyScript)
    {
        yield return new WaitForSeconds(1f);
        // dash -------------------------------------------------------------
        Vector3 playerPos = enemyScript.player.transform.position;
        Vector3 enemyPos = enemyScript.transform.position;
        Vector3 directionToPlayer = (playerPos - enemyPos).normalized;
        

        float randomRadiusPerMove = (Vector3.Distance(playerPos, enemyPos) - Random.Range(1, 2))/repeatMoveTime;
        
        for (int i = 0; i < repeatMoveTime; i++)
        {
            Vector3 targetPos = enemyPos + randomRadiusPerMove*(i+1) * (Quaternion.Euler(0, 0, Random.Range(-30f,30f)) * directionToPlayer);
            targetPos.x = Mathf.Clamp(targetPos.x,21, 40);
            targetPos.y = Mathf.Clamp(targetPos.y, 28, 44);
            enemyScript.MoveToPosition(targetPos, 0.4f, 0);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        // dash -------------------------------------------------------------


        int usingProjectileNumber = numberOfProjectile + (int)((1.0f - enemyScript.HpPercent) / 0.25f);
        int baseRepeatTime = 3;

        float radiusFromBoss = 1.5f;

        float angle = 360f / usingProjectileNumber;
        float offsetAngle = 0;
        enemyPos = enemyScript.transform.position;

        for (int t = 0; t < baseRepeatTime; t++)
        {
            for (int i = 0; i < usingProjectileNumber; i++)
            {
                GameObject projectileObject = Instantiate(orbBulletPrefab, enemyPos + radiusFromBoss * (Quaternion.Euler(0, 0, i * angle + offsetAngle) * directionToPlayer), Quaternion.identity);
                ProjectileEnemy projectile = projectileObject.GetComponent<ProjectileEnemy>();
                projectile.DelayLaunch(Quaternion.Euler(0, 0, i * angle + offsetAngle) * directionToPlayer, bulletSpeed * 100f, 1f);
            }
            offsetAngle += 20;
            yield return new WaitForSeconds(0.7f);
        }

        yield return base.Perform(enemyScript);
    }

}
