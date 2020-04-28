using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key: NonPlayerCharacter
{
    public List<Lock> lockList;
    public List<GameObject> gameObjectList;
    public bool isUnlock;
    public bool isSetActive;
    public bool isTalking;

    BoxCollider2D Collider2D;
    SpriteRenderer Renderer;

    private void Start()
    {
        Collider2D = GetComponent<BoxCollider2D>();
        Renderer = GetComponent<SpriteRenderer>();
    }

    public void unLock()
    {
        foreach(Lock l in lockList)
        {
            l.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (Collider2D != null)
            Collider2D.enabled = false;
        if (Renderer != null)
            Renderer.enabled = false;
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
        RubyController ruby = collision.GetComponent<RubyController>();
        if(ruby != null)
        {
            if (isSetActive)
            {
                foreach (GameObject o in gameObjectList)
                {
                    o.SetActive(true);
                }
                Destroy(gameObject);
        }

        }
    }

    public void setActive(bool activation)
    {
        foreach (GameObject o in gameObjectList)
        {
            o.SetActive(activation);
        }
    }
}
