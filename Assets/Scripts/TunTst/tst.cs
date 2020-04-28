using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tst : MonoBehaviour
{
    public Transform playerTransform;

    public GameObject moveObjectTest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            moveObjectTest.transform.position += Quaternion.Euler(0, 0, 30) * (playerTransform.position - transform.position);
        }

    }
}
