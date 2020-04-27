using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayOfOrb : Attack
{

    public GameObject orbPrefab;
    public int numberOfProjectile = 2;



    override public IEnumerator Perform(Enemy enemyScript)
    {
        MoveToCenter();

        int usingProjectileNumber = numberOfProjectile + (int) ((1.0f - enemyScript.HpPercent)/0.25f);
        int baseRepeatTime = 4;

        for (int t = 0; t < baseRepeatTime; t++)
        {
            for (int i = 0; i < usingProjectileNumber; i++)
            {

                GameObject projectileObject = Instantiate(orbPrefab, enemyScript.transform.position, Quaternion.identity);
                projectileObject.GetComponent<OrbController>().Setup(enemyScript.transform.position, 360 / usingProjectileNumber * i);
                yield return new WaitForSeconds(0.5f);
            }
        }


        yield return base.Perform(enemyScript);
    }

    void MoveToCenter()
    {

    }
}
