using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonProjectile : Attack
{

    public GameObject projectilePrefab;
    
    override public IEnumerator Perform(Enemy enemyScript)
    {
        
        Vector3 directionToPlayer = (enemyScript.player.transform.position - enemyScript.transform.position).normalized;
        GameObject projectileObject = Instantiate(projectilePrefab, enemyScript.transform.position + directionToPlayer, Quaternion.identity, enemyScript.transform);
        ProjectileEnemy projectile = projectileObject.GetComponent<ProjectileEnemy>();

        projectile.Launch(directionToPlayer, 300);
        return base.Perform(enemyScript);
    }

}
