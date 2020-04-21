using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public float executionTime;
    public string attackName;

    virtual public IEnumerator Perform(Enemy enemyScript)
    {
        
        yield return new WaitForSeconds(executionTime);
        Debug.Log("finish an attack : " + attackName);
        enemyScript.nextAttackReady = true;
        yield return null;
    }
}
