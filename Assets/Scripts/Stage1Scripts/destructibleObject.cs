using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructibleObject : MonoBehaviour
{
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeHealth(int number)
    {
        health += number;
        if(health <= 0)
        {
            //destroy 
            Destroy(gameObject);
        }
    }
    
}
