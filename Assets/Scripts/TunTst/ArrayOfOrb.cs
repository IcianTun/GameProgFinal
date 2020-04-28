using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayOfOrb : Attack
{

    public GameObject orbPrefab;
    public int numberOfProjectile = 2;



    override public IEnumerator Perform(Enemy enemyScript)
    {
        yield return new WaitForSeconds(1f);
        TunBoss tunBoss = (TunBoss)enemyScript;
        if (tunBoss.center)
        {
            tunBoss.MoveToPosition(tunBoss.center.position, 0.5f, 0);
            yield return new WaitForSeconds(1f);
        }


        int usingProjectileNumber = numberOfProjectile + (int) ((1.0f - enemyScript.HpPercent)/0.25f);
        int baseRepeatTime = 5;

        for (int t = 0; t < baseRepeatTime; t++)
        {
            for (int i = 0; i < usingProjectileNumber; i++)
            {

                GameObject projectileObject = Instantiate(orbPrefab, enemyScript.transform.position, Quaternion.identity);
                projectileObject.GetComponent<OrbController>().Setup(enemyScript.transform.position, 360 / usingProjectileNumber * i);
            }
            yield return new WaitForSeconds(0.5f);
        }


        yield return base.Perform(enemyScript);
    }
    
}
