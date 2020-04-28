using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfWall : Attack
{

    public GameObject projectilePrefab;
    public GameObject targetTelegraph;


    override public IEnumerator Perform(Enemy enemyScript)
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 playerPos = enemyScript.player.transform.position;
        Vector3 enemyPos = enemyScript.transform.position;
        Vector3 directionToPlayer = (playerPos - enemyPos).normalized;

        int numberOfProjectile = 13;
        Instantiate(targetTelegraph, playerPos, Quaternion.identity).GetComponent<TargetTelegraph>().SetDestroyTimer(2f);
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < numberOfProjectile; i++)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, enemyPos + (i+2) * directionToPlayer, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }


        yield return base.Perform(enemyScript);
    }


}
