using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key: NonPlayerCharacter
{
    public List<Lock> lockList;

    public void unLock()
    {
        foreach(Lock l in lockList)
        {
            l.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    public override void DisplayDialog()
    {
        base.DisplayDialog();
        unLock();
    }
}
