using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key: NonPlayerCharacter
{
    public List<Lock> lockList;
    public List<GameObject> gameObjectList;
    public bool isUnlock;
    public bool isManifest;
    public bool isTalking;
    public void unLock()
    {
        foreach(Lock l in lockList)
        {
            l.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public override void DisplayDialog()
    {
        if (isTalking)
        {
            base.DisplayDialog();
        }
        if (isUnlock)
        {
            unLock();
        }
        else
        {
            createLock();
        }
    }


    public void createLock()
    {
        foreach (Lock l in lockList)
        {
            l.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isManifest)
        {
            foreach (GameObject o in gameObjectList)
            {
                o.SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
