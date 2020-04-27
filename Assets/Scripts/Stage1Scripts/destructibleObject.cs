using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructibleObject : MonoBehaviour
{
    public int health;

    public GameObject hiddenItem;

    public void changeHealth(int number)
    {
        health += number;
        if(health <= 0)
        {
            //destroy 
            if (hiddenItem != null)
                hiddenItem.SetActive(true);
            Destroy(gameObject);
        }
    }
    
}
