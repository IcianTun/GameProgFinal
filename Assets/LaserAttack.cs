using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : Attack
{
    public GameObject laserPrefab;
    public GameObject targetTelegraph;
    

    override public IEnumerator Perform(Enemy enemyScript)
    {
        GameObject player = enemyScript.player;
        Instantiate(targetTelegraph, player.transform.position, Quaternion.identity).GetComponent<TargetTelegraph>().SetDestroyTimer(executionTime);
        GameObject laserObj = Instantiate(laserPrefab, player.transform.position - enemyScript.transform.position, Quaternion.identity);
        laserObj.GetComponent<Laser>();

        return base.Perform(enemyScript);
    }

}
