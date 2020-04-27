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
        Vector2 directionToPlayer = (player.transform.position - enemyScript.transform.position).normalized;
        Vector3 posBetween = (player.transform.position + enemyScript.transform.position) / 2;

        Instantiate(targetTelegraph, player.transform.position, Quaternion.identity).GetComponent<TargetTelegraph>().SetDestroyTimer(executionTime);
        
        StartCoroutine(SummonAfter(directionToPlayer,posBetween));

        return base.Perform(enemyScript);
    }

    IEnumerator SummonAfter(Vector2 directionToPlayer, Vector3 posBetween)
    {
        yield return new WaitForSeconds(Mathf.Max(executionTime-1,0));
        GameObject laserObj = Instantiate(laserPrefab);
        laserObj.transform.up = directionToPlayer;
        laserObj.transform.position = posBetween;

        laserObj.GetComponent<Laser>().DelayLaunch(directionToPlayer, 1);
    }
}
