using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : NonPlayerCharacter
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("locker room enter");
        NonPlayerCharacter npc = gameObject.GetComponent<NonPlayerCharacter>();
        if(npc != null)
        {
            npc.DisplayDialog();
        }
    }
}
